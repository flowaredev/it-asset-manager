using MiniExcelLibs;
using ITAssetManagerLibrary.Data;
using ITAssetManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagerLibrary.Services
{
    public interface IExcelDataService
    {
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
                "Server",
                "Storage", 
                "BackupEquipment",
                "NetworkEquipment",
                "SecurityEquipment",
                "Software",
                "SupportEquipment",
                "MiscellaneousEquipment",
                "Failure",
                "SecurityVulnerability",
                "RoutineCheck",
                "Maintenance",
                "ServiceLevelAgreement",
            });
        }

        public async Task<string> ExportToExcelAsync<T>(string entityType) where T : class
        {
            using var context = _dbContextFactory.CreateDbContext();
            var memoryStream = new MemoryStream();

            if (entityType == "Server")
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
                        sd.Cpu,
                        sd.Ram,
                        sd.Disk,
                        sd.Rack
                    })
                    .ToListAsync();

                // 멀티 시트 엑셀 파일 생성
                var sheets = new Dictionary<string, object>
                {
                    ["Servers"] = serverData,
                    ["ServerDevices"] = serverDeviceData
                };

                await memoryStream.SaveAsAsync(sheets);
            }
            else
            {
                object data = entityType switch
                {
                    "Storage" => await context.Storages.Include(s => s.CommonAsset).Select(s => new
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
                    "BackupEquipment" => await context.BackupEquipments.Include(b => b.CommonAsset).Select(b => new
                    {
                        b.Id,
                        b.CommonAsset.ManagementTag,
                        b.CommonAsset.Name,
                        b.CommonAsset.Role,
                        b.CommonAsset.ApplyDateTime,
                        b.CommonAsset.ResponsibleCompany,
                        b.CommonAsset.ResponsiblePerson,
                        b.CommonAsset.ResponsiblePersonPhone,
                        b.CommonAsset.OnSiteManager,
                        b.CommonAsset.OnSiteManagerPhone
                    }).ToListAsync(),
                    "NetworkEquipment" => await context.NetworkEquipments.Include(n => n.CommonAsset).Select(n => new
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
                    "SecurityEquipment" => await context.SecurityEquipments.Include(s => s.CommonAsset).Select(s => new
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
                    "Software" => await context.Softwares.Include(s => s.CommonAsset).Select(s => new
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
                    "SupportEquipment" => await context.SupportEquipments.Include(s => s.CommonAsset).Select(s => new
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
                    "MiscellaneousEquipment" => await context.MiscellaneousEquipments.Include(m => m.CommonAsset).Select(m => new
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
                    "Failure" => await context.Failures.Include(f => f.CommonAsset).Select(f => new
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
                    "SecurityVulnerability" => await context.SecurityVulnerabilities.Include(s => s.CommonAsset).Select(s => new
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
                    "RoutineCheck" => await context.RoutineChecks.Include(r => r.CommonAsset).Select(r => new
                    {
                        r.Id,
                        r.Detail,
                        r.StartDateTime,
                        r.EndDateTime,
                        AssetManagementTag = r.CommonAsset.ManagementTag,
                        AssetName = r.CommonAsset.Name,
                        AssetRole = r.CommonAsset.Role
                    }).ToListAsync(),
                    "Maintenance" => await context.Maintenances.ToListAsync(),
                    "ServiceLevelAgreement" => await context.ServiceLevelAgreements.ToListAsync(),
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
                case "Server":
                    context.Servers.AddRange(rows as IEnumerable<Server> ?? new List<Server>());
                    break;
                case "Storage":
                    context.Storages.AddRange(rows as IEnumerable<Storage> ?? new List<Storage>());
                    break;
                case "BackupEquipment":
                    context.BackupEquipments.AddRange(rows as IEnumerable<BackupEquipment> ?? new List<BackupEquipment>());
                    break;
                case "NetworkEquipment":
                    context.NetworkEquipments.AddRange(rows as IEnumerable<NetworkEquipment> ?? new List<NetworkEquipment>());
                    break;
                case "SecurityEquipment":
                    context.SecurityEquipments.AddRange(rows as IEnumerable<SecurityEquipment> ?? new List<SecurityEquipment>());
                    break;
                case "Software":
                    context.Softwares.AddRange(rows as IEnumerable<Software> ?? new List<Software>());
                    break;
                case "SupportEquipment":
                    context.SupportEquipments.AddRange(rows as IEnumerable<SupportEquipment> ?? new List<SupportEquipment>());
                    break;
                case "MiscellaneousEquipment":
                    context.MiscellaneousEquipments.AddRange(rows as IEnumerable<MiscellaneousEquipment> ?? new List<MiscellaneousEquipment>());
                    break;
                case "Failure":
                    context.Failures.AddRange(rows as IEnumerable<Failure> ?? new List<Failure>());
                    break;
                case "SecurityVulnerability":
                    context.SecurityVulnerabilities.AddRange(rows as IEnumerable<SecurityVulnerability> ?? new List<SecurityVulnerability>());
                    break;
                case "RoutineCheck":
                    context.RoutineChecks.AddRange(rows as IEnumerable<RoutineCheck> ?? new List<RoutineCheck>());
                    break;
                case "Maintenance":
                    context.Maintenances.AddRange(rows as IEnumerable<Maintenance> ?? new List<Maintenance>());
                    break;
                case "ServiceLevelAgreement":
                    context.ServiceLevelAgreements.AddRange(rows as IEnumerable<ServiceLevelAgreement> ?? new List<ServiceLevelAgreement>());
                    break;               
                default:
                    throw new ArgumentException($"Unknown entity type: {entityType}");
            }

            return await context.SaveChangesAsync();
        }
    }
}