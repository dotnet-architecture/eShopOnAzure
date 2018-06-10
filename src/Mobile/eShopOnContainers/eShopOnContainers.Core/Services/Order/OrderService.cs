﻿using eShopOnContainers.Core.Models.Basket;
using eShopOnContainers.Core.Services.RequestProvider;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace eShopOnContainers.Core.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IRequestProvider _requestProvider;

        public OrderService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Models.Orders.Order>> GetOrdersAsync(string token)
        {
        
            UriBuilder builder = new UriBuilder(GlobalSetting.Instance.OrdersEndpoint);

            builder.Path = "api/v1/orders";

            string uri = builder.ToString();

            ObservableCollection<Models.Orders.Order> orders =
                await _requestProvider.GetAsync<ObservableCollection<Models.Orders.Order>>(uri, token);

            return orders;
            
        }

        public async Task<Models.Orders.Order> GetOrderAsync(int orderId, string token)
        {
            try
            {
                UriBuilder builder = new UriBuilder(GlobalSetting.Instance.OrdersEndpoint);

                builder.Path = string.Format("api/v1/orders/{0}", orderId);

                string uri = builder.ToString();

                Models.Orders.Order order =
                    await _requestProvider.GetAsync<Models.Orders.Order>(uri, token);

                return order;
            }
            catch
            {
                return new Models.Orders.Order();
            }
        }

        public async Task<ObservableCollection<Models.Orders.CardType>> GetCardTypesAsync(string token)
        {
            try
            {
                UriBuilder builder = new UriBuilder(GlobalSetting.Instance.OrdersEndpoint);

                builder.Path = "api/v1/orders/cardtypes";

                string uri = builder.ToString();

                ObservableCollection<Models.Orders.CardType> cardTypes =
                    await _requestProvider.GetAsync<ObservableCollection<Models.Orders.CardType>>(uri, token);

                return cardTypes;
            }
            catch
            {
                return new ObservableCollection<Models.Orders.CardType>();
            }
        }

        public BasketCheckout MapOrderToBasket(Models.Orders.Order order)
        {
            return new BasketCheckout()
            {
                CardExpiration = order.CardExpiration,
                CardHolderName = order.CardHolderName,
                CardNumber = order.CardNumber,
                CardSecurityNumber = order.CardSecurityNumber,
                CardTypeId = order.CardTypeId,
                City = order.ShippingCity,
                Country = order.ShippingCountry,
                ZipCode = order.ShippingZipCode,
                Street = order.ShippingStreet
            };
        }
    }
}