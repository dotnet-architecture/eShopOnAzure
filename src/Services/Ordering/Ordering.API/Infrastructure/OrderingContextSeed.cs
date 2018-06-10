﻿namespace Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure
{
    using AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.eShopOnContainers.Services.Ordering.Domain;
    using Ordering.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
    using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;

    public class OrderingContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            var context = (OrderingContext)applicationBuilder
                .ApplicationServices.GetService(typeof(OrderingContext));

            using (context)
            {
                context.Database.Migrate();

                if (!context.CardTypes.Any())
                {
                    context.CardTypes.Add(CardType.Amex);
                    context.CardTypes.Add(CardType.Visa);
                    context.CardTypes.Add(CardType.MasterCard);

                    await context.SaveChangesAsync();
                }

                if (!context.OrderStatus.Any())
                {
                    context.OrderStatus.Add(OrderStatus.Submitted);
                    context.OrderStatus.Add(OrderStatus.AwaitingValidation);
                    context.OrderStatus.Add(OrderStatus.StockConfirmed);
                    context.OrderStatus.Add(OrderStatus.Paid);
                    context.OrderStatus.Add(OrderStatus.Shipped);
                    context.OrderStatus.Add(OrderStatus.Cancelled);
                }

                await context.SaveChangesAsync();
            }
        }

    }
}
