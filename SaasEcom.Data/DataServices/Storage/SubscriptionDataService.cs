﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SaasEcom.Data.DataServices.Interfaces;
using SaasEcom.Data.Models;

namespace SaasEcom.Data.DataServices.Storage
{
    public class SubscriptionDataService : ISubscriptionService
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionDataService(ApplicationDbContext context)
        {
            this._dbContext = context;
        }

        public async Task<Subscription> SubscribeUserAsync(ApplicationUser user, string planId)
        {
            var plan = await _dbContext.SubscriptionPlans.FirstAsync(x => x.FriendlyId == planId);

            var s = new Subscription
            {
                Start = DateTime.UtcNow,
                End = null,
                TrialEnd = DateTime.UtcNow.AddDays(plan.TrialPeriodInDays),
                TrialStart = DateTime.UtcNow,
                User = user,
                SubscriptionPlan = plan
            };

            _dbContext.Subscriptions.Add(s);
            await _dbContext.SaveChangesAsync();

            return s;
        }

        public async Task<Subscription> UserActiveSubscriptionAsync(string userId)
        {
            return await _dbContext.Subscriptions.Where(s => s.User.Id == userId && s.End == null).FirstOrDefaultAsync();
        }

        public async Task<List<Subscription>> UserSubscriptionsAsync(string userId)
        {
            return await _dbContext.Subscriptions.Where(s => s.User.Id == userId).Select(s => s).ToListAsync();
        }

        public async Task EndSubscriptionAsync(int subscriptionId)
        {
            var dbSub = await _dbContext.Subscriptions.FindAsync(subscriptionId);
            dbSub.End = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            _dbContext.Entry(subscription).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        
        // TODO: Implement Upgrade and downgrade!!
        
    }
}
