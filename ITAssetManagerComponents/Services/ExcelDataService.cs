using MiniExcelLibs;
using ITAssetManagerLibrary.Data;
using ITAssetManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagerComponents.Services
{
    public interface IExcelDataService
    {
        const string SERVER_KEY = "Server";
        const string SERVER_DEVICE_KEY = "ServerDevice";
        const string SERVER_ROUTINE_CHECKS_KEY = "ServerRoutineChecks";
        const string SERVER_SECURITY_VULNERABILITIES_KEY = "ServerSecurityVulnerabilities";
        const string SERVER_MAINTENANCES_KEY = "ServerMaintenances";
        const string SERVER_FAILURES_KEY = "ServerFailures";
        const string STORAGE_KEY = "Storage";
        const string NETWORK_EQUIPMENT_KEY = "NetworkEquipment";
        const string SECURITY_EQUIPMENT_KEY = "SecurityEquipment";
        const string SOFTWARE_KEY = "Software";
        const string SUPPORT_EQUIPMENT_KEY = "SupportEquipment";
        const string MISCELLANEOUS_EQUIPMENT_KEY = "MiscellaneousEquipment";
        const string FAILURE_KEY = "Failure";
        const string SECURITY_VULNERABILITY_KEY = "SecurityVulnerability";
        const string ROUTINE_CHECK_KEY = "RoutineCheck";
        const string MAINTENANCE_KEY = "Maintenance";

        Task<string> ExportToExcelAsync<T>(string entityType) where T : class;
        Task<List<string>> GetAvailableEntityTypesAsync();
        Task<int> ImportFromExcelAsync<T>(Stream excelStream, string entityType) where T : class, new();
    }

    public class ExcelDataService : IExcelDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ExcelDataService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<List<string>> GetAvailableEntityTypesAsync()
        {
            return Task.FromResult(new List<string>
            {
                IExcelDataService.SERVER_KEY,
                IExcelDataService.STORAGE_KEY,
                IExcelDataService.NETWORK_EQUIPMENT_KEY,
                IExcelDataService.SECURITY_EQUIPMENT_KEY,
                IExcelDataService.SOFTWARE_KEY,
                IExcelDataService.SUPPORT_EQUIPMENT_KEY,
                IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY,
                IExcelDataService.FAILURE_KEY,
                IExcelDataService.SECURITY_VULNERABILITY_KEY,
                IExcelDataService.ROUTINE_CHECK_KEY,
                IExcelDataService.MAINTENANCE_KEY,
            });
        }

        public async Task<string> ExportToExcelAsync<T>(string entityType) where T : class
        {
            using var context = _dbContextFactory.CreateDbContext();
            var memoryStream = new MemoryStream();

            if (entityType == IExcelDataService.SERVER_KEY)
            {
                // Server 데이터와 CommonAsset을 Join하여 플래튼된 데이터 생성
                var serverData = await context.Servers
                    .Include(s => s.CommonAsset)
                    .Include(s => s.ServerDevices)
                    .Select(s => new
                    {
                        s.Id,
                        s.CommonAsset.ManagementTag,
                        s.CommonAsset.Name,
                        s.CommonAsset.Role,
                        s.CommonAsset.ApplyDateTime,
                        s.CommonAsset.ResponsibleCompany,
                        s.CommonAsset.ResponsiblePerson,
                        s.CommonAsset.ResponsiblePersonPhone,
                        s.CommonAsset.OnSiteManager,
                        s.CommonAsset.OnSiteManagerPhone
                    })
                    .ToListAsync();

                // ServerDevice 데이터 생성 (Server Id 참조 포함)
                var serverDeviceData = await context.ServerDevices
                    .Include(sd => sd.Server)
                    .Select(sd => new
                    {
                        ServerId = sd.ServerId, // 첫 번째 시트의 Id 참조
                        sd.Id,
                        sd.Manufacturer,
                        sd.Model,
                        sd.SerialNumber,
                        sd.Ram,
                        sd.Disk,
                        sd.Rack
                    })
                    .ToListAsync();

                // Server와 관련된 RoutineCheck 데이터
                var serverRoutineChecksData = await context.RoutineChecks
                    .Include(rc => rc.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(rc => rc.CommonAsset.Server != null)
                    .Select(rc => new
                    {
                        ServerId = rc.CommonAsset.Server!.Id, // Server Id 참조
                        rc.Id,
                        rc.Detail,
                        rc.StartDateTime,
                        rc.EndDateTime,
                        AssetManagementTag = rc.CommonAsset.ManagementTag,
                        AssetName = rc.CommonAsset.Name,
                        AssetRole = rc.CommonAsset.Role
                    })
                    .ToListAsync();

                // Server와 관련된 SecurityVulnerability 데이터
                var serverSecurityVulnerabilitiesData = await context.SecurityVulnerabilities
                    .Include(sv => sv.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(sv => sv.CommonAsset.Server != null)
                    .Select(sv => new
                    {
                        ServerId = sv.CommonAsset.Server!.Id, // Server Id 참조
                        sv.Id,
                        sv.DiscoveryDateTime,
                        sv.VulnerabilityDetail,
                        sv.VisitDateTime,
                        sv.ResolveDateTime,
                        sv.TaskDetail,
                        sv.IsResolved,
                        sv.Level,
                        AssetManagementTag = sv.CommonAsset.ManagementTag,
                        AssetName = sv.CommonAsset.Name,
                        AssetRole = sv.CommonAsset.Role
                    })
                    .ToListAsync();

                // Server와 관련된 Maintenance 데이터
                var serverMaintenancesData = await context.Maintenances
                    .Include(m => m.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(m => m.CommonAsset.Server != null)
                    .Select(m => new
                    {
                        ServerId = m.CommonAsset.Server!.Id, // Server Id 참조
                        m.Id,
                        m.Description,
                        m.VisitDateTime,
                        m.ResolveDateTime,
                        AssetManagementTag = m.CommonAsset.ManagementTag,
                        AssetName = m.CommonAsset.Name,
                        AssetRole = m.CommonAsset.Role
                    })
                    .ToListAsync();

                // Server와 관련된 Failure 데이터
                var serverFailuresData = await context.Failures
                    .Include(f => f.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(f => f.CommonAsset.Server != null)
                    .Select(f => new
                    {
                        ServerId = f.CommonAsset.Server!.Id, // Server Id 참조
                        f.Id,
                        f.FailureDateTime,
                        f.Description,
                        f.VisitDateTime,
                        f.ResolveDateTime,
                        f.DisabilityHours,
                        f.ResolveDescription,
                        f.IsResolved,
                        AssetManagementTag = f.CommonAsset.ManagementTag,
                        AssetName = f.CommonAsset.Name,
                        AssetRole = f.CommonAsset.Role
                    })
                    .ToListAsync();

                // 멀티 시트 엑셀 파일 생성
                var sheets = new Dictionary<string, object>
                {
                    [IExcelDataService.SERVER_KEY] = serverData,
                    [IExcelDataService.SERVER_DEVICE_KEY] = serverDeviceData,
                    [IExcelDataService.SERVER_ROUTINE_CHECKS_KEY] = serverRoutineChecksData,
                    [IExcelDataService.SERVER_SECURITY_VULNERABILITIES_KEY] = serverSecurityVulnerabilitiesData,
                    [IExcelDataService.SERVER_MAINTENANCES_KEY] = serverMaintenancesData,
                    [IExcelDataService.SERVER_FAILURES_KEY] = serverFailuresData
                };

                await memoryStream.SaveAsAsync(sheets);
            }
            else
            {
                object data = entityType switch
                {
                    IExcelDataService.STORAGE_KEY => await context.Storages.Include(s => s.CommonAsset).Select(s => new
                    {
                        s.Id,
                        s.CommonAsset.ManagementTag,
                        s.CommonAsset.Name,
                        s.CommonAsset.Role,
                        s.CommonAsset.ApplyDateTime,
                        s.CommonAsset.ResponsibleCompany,
                        s.CommonAsset.ResponsiblePerson,
                        s.CommonAsset.ResponsiblePersonPhone,
                        s.CommonAsset.OnSiteManager,
                        s.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    IExcelDataService.NETWORK_EQUIPMENT_KEY => await context.NetworkEquipments.Include(n => n.CommonAsset).Select(n => new
                    {
                        n.Id,
                        n.CommonAsset.ManagementTag,
                        n.CommonAsset.Name,
                        n.CommonAsset.Role,
                        n.CommonAsset.ApplyDateTime,
                        n.CommonAsset.ResponsibleCompany,
                        n.CommonAsset.ResponsiblePerson,
                        n.CommonAsset.ResponsiblePersonPhone,
                        n.CommonAsset.OnSiteManager,
                        n.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    IExcelDataService.SECURITY_EQUIPMENT_KEY => await context.SecurityEquipments.Include(s => s.CommonAsset).Select(s => new
                    {
                        s.Id,
                        s.CommonAsset.ManagementTag,
                        s.CommonAsset.Name,
                        s.CommonAsset.Role,
                        s.CommonAsset.ApplyDateTime,
                        s.CommonAsset.ResponsibleCompany,
                        s.CommonAsset.ResponsiblePerson,
                        s.CommonAsset.ResponsiblePersonPhone,
                        s.CommonAsset.OnSiteManager,
                        s.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    IExcelDataService.SOFTWARE_KEY => await context.Softwares.Include(s => s.CommonAsset).Select(s => new
                    {
                        s.Id,
                        s.CommonAsset.ManagementTag,
                        s.CommonAsset.Name,
                        s.CommonAsset.Role,
                        s.CommonAsset.ApplyDateTime,
                        s.CommonAsset.ResponsibleCompany,
                        s.CommonAsset.ResponsiblePerson,
                        s.CommonAsset.ResponsiblePersonPhone,
                        s.CommonAsset.OnSiteManager,
                        s.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    IExcelDataService.SUPPORT_EQUIPMENT_KEY => await context.SupportEquipments.Include(s => s.CommonAsset).Select(s => new
                    {
                        s.Id,
                        s.CommonAsset.ManagementTag,
                        s.CommonAsset.Name,
                        s.CommonAsset.Role,
                        s.CommonAsset.ApplyDateTime,
                        s.CommonAsset.ResponsibleCompany,
                        s.CommonAsset.ResponsiblePerson,
                        s.CommonAsset.ResponsiblePersonPhone,
                        s.CommonAsset.OnSiteManager,
                        s.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY => await context.MiscellaneousEquipments.Include(m => m.CommonAsset).Select(m => new
                    {
                        m.Id,
                        m.CommonAsset.ManagementTag,
                        m.CommonAsset.Name,
                        m.CommonAsset.Role,
                        m.CommonAsset.ApplyDateTime,
                        m.CommonAsset.ResponsibleCompany,
                        m.CommonAsset.ResponsiblePerson,
                        m.CommonAsset.ResponsiblePersonPhone,
                        m.CommonAsset.OnSiteManager,
                        m.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    IExcelDataService.FAILURE_KEY => await context.Failures.Include(f => f.CommonAsset).Select(f => new
                    {
                        f.Id,
                        f.FailureDateTime,
                        f.Description,
                        f.VisitDateTime,
                        f.ResolveDateTime,
                        f.DisabilityHours,
                        f.ResolveDescription,
                        f.IsResolved,
                        AssetManagementTag = f.CommonAsset.ManagementTag,
                        AssetName = f.CommonAsset.Name,
                        AssetRole = f.CommonAsset.Role
                    }).ToListAsync(),
                    IExcelDataService.SECURITY_VULNERABILITY_KEY => await context.SecurityVulnerabilities.Include(s => s.CommonAsset).Select(s => new
                    {
                        s.Id,
                        s.DiscoveryDateTime,
                        s.VulnerabilityDetail,
                        s.VisitDateTime,
                        s.ResolveDateTime,
                        s.TaskDetail,
                        s.IsResolved,
                        s.Level,
                        AssetManagementTag = s.CommonAsset.ManagementTag,
                        AssetName = s.CommonAsset.Name,
                        AssetRole = s.CommonAsset.Role
                    }).ToListAsync(),
                    IExcelDataService.ROUTINE_CHECK_KEY => await context.RoutineChecks.Include(r => r.CommonAsset).Select(r => new
                    {
                        r.Id,
                        r.Detail,
                        r.StartDateTime,
                        r.EndDateTime,
                        AssetManagementTag = r.CommonAsset.ManagementTag,
                        AssetName = r.CommonAsset.Name,
                        AssetRole = r.CommonAsset.Role
                    }).ToListAsync(),
                    IExcelDataService.MAINTENANCE_KEY => await context.Maintenances.Include(m => m.CommonAsset).Select(m => new
                    {
                        m.Id,
                        m.Description,
                        m.VisitDateTime,
                        m.ResolveDateTime,
                        AssetManagementTag = m.CommonAsset.ManagementTag,
                        AssetName = m.CommonAsset.Name,
                        AssetRole = m.CommonAsset.Role
                    }).ToListAsync(),
                    _ => throw new ArgumentException($"Unknown entity type: {entityType}")
                };

                await memoryStream.SaveAsAsync(data);
            }

            var bytes = memoryStream.ToArray();
            return Convert.ToBase64String(bytes);
        }

        public async Task<int> ImportFromExcelAsync<T>(Stream excelStream, string entityType) where T : class, new()
        {
            using var context = _dbContextFactory.CreateDbContext();

            var rows = excelStream.Query<T>().ToList();

            switch (entityType)
            {
                case IExcelDataService.SERVER_KEY:
                    context.Servers.AddRange(rows as IEnumerable<Server> ?? new List<Server>());
                    break;
                case IExcelDataService.STORAGE_KEY:
                    context.Storages.AddRange(rows as IEnumerable<Storage> ?? new List<Storage>());
                    break;
                case IExcelDataService.NETWORK_EQUIPMENT_KEY:
                    context.NetworkEquipments.AddRange(rows as IEnumerable<NetworkEquipment> ?? new List<NetworkEquipment>());
                    break;
                case IExcelDataService.SECURITY_EQUIPMENT_KEY:
                    context.SecurityEquipments.AddRange(rows as IEnumerable<SecurityEquipment> ?? new List<SecurityEquipment>());
                    break;
                case IExcelDataService.SOFTWARE_KEY:
                    context.Softwares.AddRange(rows as IEnumerable<Software> ?? new List<Software>());
                    break;
                case IExcelDataService.SUPPORT_EQUIPMENT_KEY:
                    context.SupportEquipments.AddRange(rows as IEnumerable<SupportEquipment> ?? new List<SupportEquipment>());
                    break;
                case IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY:
                    context.MiscellaneousEquipments.AddRange(rows as IEnumerable<MiscellaneousEquipment> ?? new List<MiscellaneousEquipment>());
                    break;
                case IExcelDataService.FAILURE_KEY:
                    context.Failures.AddRange(rows as IEnumerable<Failure> ?? new List<Failure>());
                    break;
                case IExcelDataService.SECURITY_VULNERABILITY_KEY:
                    context.SecurityVulnerabilities.AddRange(rows as IEnumerable<SecurityVulnerability> ?? new List<SecurityVulnerability>());
                    break;
                case IExcelDataService.ROUTINE_CHECK_KEY:
                    context.RoutineChecks.AddRange(rows as IEnumerable<RoutineCheck> ?? new List<RoutineCheck>());
                    break;
                case IExcelDataService.MAINTENANCE_KEY:
                    context.Maintenances.AddRange(rows as IEnumerable<Maintenance> ?? new List<Maintenance>());
                    break;
                default:
                    throw new ArgumentException($"Unknown entity type: {entityType}");
            }

            return await context.SaveChangesAsync();
        }
    }
}