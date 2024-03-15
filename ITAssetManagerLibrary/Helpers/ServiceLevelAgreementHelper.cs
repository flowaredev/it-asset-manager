using ITAssetManagerLibrary.Data;
using ITAssetManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Helpers
{
    public class ServiceLevelAgreementHelper
    {
        public static async Task UpdateFailure(IDbContextFactory<ApplicationDbContext> dbFactory, DateTime failureDateTime)
        {
            using var context = dbFactory.CreateDbContext();

            var failuresInMonth = context.Failures
                    .Where(f => f.FailureDateTime.Year == failureDateTime.Year
                        && f.FailureDateTime.Month == failureDateTime.Month);

            var endDays = DateTime.DaysInMonth(failureDateTime.Year, failureDateTime.Month);
            var disabiltiyHoursAverage = await failuresInMonth.AverageAsync(f => f.DisabilityHours);
            var disabiltiyHoursLevel = disabiltiyHoursAverage switch
            {
                var hours when hours >= 0 && hours <= 4 => ServiceLevel.Excellent,
                var hours when hours <= 6 => ServiceLevel.Good,
                var hours when hours <= 8 => ServiceLevel.Average,
                var hours when hours <= 10 => ServiceLevel.Insufficient,
                var hours when hours > 10 => ServiceLevel.Poor,
                _ => ServiceLevel.None
            };

            var uptimeRate = 1 - ((await failuresInMonth.ToArrayAsync())
                .Sum(f => (f.ResolveDateTime - f.FailureDateTime).TotalHours) / (24 * endDays));

            var uptimeLevel = uptimeRate switch
            {
                var rate when rate >= 1 => ServiceLevel.Excellent,
                var rate when rate >= 0.98 => ServiceLevel.Good,
                var rate when rate >= 0.94 => ServiceLevel.Average,
                var rate when rate >= 0.92 => ServiceLevel.Insufficient,
                var rate when rate < 0.92 => ServiceLevel.Poor,
                _ => ServiceLevel.None
            };

            var resolvedIn48HourCount = await failuresInMonth.CountAsync(f => f.IsResolved && f.DisabilityHours <= 48);
            var failureCount = await failuresInMonth.CountAsync();
            var technicalSupportCompletionRate = (double)resolvedIn48HourCount / failureCount;
            var technicalSupportLevel = technicalSupportCompletionRate switch
            {
                var rate when rate >= 0.98 => ServiceLevel.Excellent,
                var rate when rate >= 0.94 => ServiceLevel.Good,
                var rate when rate >= 0.92 => ServiceLevel.Average,
                var rate when rate >= 0.9 => ServiceLevel.Insufficient,
                var rate when rate < 0.9 => ServiceLevel.Poor,
                _ => ServiceLevel.None
            };

            var serviceLevelAgreement = await context.ServiceLevelAgreements
                .SingleOrDefaultAsync(s => s.EndDateOfMonth.Year == failureDateTime.Year
                    && s.EndDateOfMonth.Month == failureDateTime.Month);

            if (serviceLevelAgreement == null)
            {
                var newServiceLevelAgreement = new ServiceLevelAgreement
                {
                    EndDateOfMonth = new DateOnly(failureDateTime.Year, failureDateTime.Month, endDays),
                    DisabilityHoursAverage = disabiltiyHoursAverage,
                    DisabilityHoursLevel = disabiltiyHoursLevel,
                    UptimeRate = uptimeRate,
                    UptimeLevel = uptimeLevel,
                    TechnicalSupportCompletionRate = technicalSupportCompletionRate,
                    TechnicalSupportLevel = technicalSupportLevel,
                };

                await context.ServiceLevelAgreements.AddAsync(newServiceLevelAgreement);
            }
            else
            {
                serviceLevelAgreement.DisabilityHoursAverage = disabiltiyHoursAverage;
                serviceLevelAgreement.DisabilityHoursLevel = disabiltiyHoursLevel;
                serviceLevelAgreement.UptimeRate = uptimeRate;
                serviceLevelAgreement.UptimeLevel = uptimeLevel;
                serviceLevelAgreement.TechnicalSupportCompletionRate = technicalSupportCompletionRate;
                serviceLevelAgreement.TechnicalSupportLevel = technicalSupportLevel;

                context.ServiceLevelAgreements.Update(serviceLevelAgreement);
            }

            await context.SaveChangesAsync();
        }

        public static async Task UpdateSecurityVulnerability(IDbContextFactory<ApplicationDbContext> dbFactory, DateTime discoveryDateTime)
        {
            using var context = dbFactory.CreateDbContext();
            var securityVulnerabilitiesInMonth = context.SecurityVulnerabilities
                .Where(s => s.DiscoveryDateTime.Year == discoveryDateTime.Year
                                   && s.DiscoveryDateTime.Month == discoveryDateTime.Month);
            var endDays = DateTime.DaysInMonth(discoveryDateTime.Year, discoveryDateTime.Month);
            var lowCount = await securityVulnerabilitiesInMonth.CountAsync(s => s.Level == SecurityVulnerabilityLevel.Low);
            var mediumCount = await securityVulnerabilitiesInMonth.CountAsync(s => s.Level == SecurityVulnerabilityLevel.Medium);
            var securityVulnerabilityServiceLevel = (lowCount, mediumCount) switch
            {
                ( >= 0, >= 1) => ServiceLevel.Poor,
                ( > 3, 0) => ServiceLevel.Poor,
                (3, 0) => ServiceLevel.Insufficient,
                (2, 0) => ServiceLevel.Average,
                (1, 0) => ServiceLevel.Good,
                (0, 0) => ServiceLevel.Excellent,
                _ => ServiceLevel.None
            };

            var serviceLevelAgreement = await context.ServiceLevelAgreements
                .SingleOrDefaultAsync(s => s.EndDateOfMonth.Year == discoveryDateTime.Year
                                   && s.EndDateOfMonth.Month == discoveryDateTime.Month);

            if (serviceLevelAgreement == null)
            {
                var newServiceLevelAgreement = new ServiceLevelAgreement
                {
                    EndDateOfMonth = new DateOnly(discoveryDateTime.Year, discoveryDateTime.Month, endDays),
                    SecurityIssues = lowCount + mediumCount,
                    SecurityIssuesLevel = securityVulnerabilityServiceLevel
                };

                await context.ServiceLevelAgreements.AddAsync(newServiceLevelAgreement);
            }
            else
            {
                serviceLevelAgreement.SecurityIssues = lowCount + mediumCount;
                serviceLevelAgreement.SecurityIssuesLevel = securityVulnerabilityServiceLevel;
                context.ServiceLevelAgreements.Update(serviceLevelAgreement);
            }

            await context.SaveChangesAsync();
        }
    }
}
