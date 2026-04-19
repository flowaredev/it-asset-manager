using MiniExcelLibs;
using ITAssetManagerLibrary.Data;
using ITAssetManagerLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagerComponents.Services
{
    public interface IExcelDataService
    {
        const string COMMON_ASSET_KEY = "CommonAsset";
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
                IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY
            });
        }

        public async Task<string> ExportToExcelAsync<T>(string entityType) where T : class
        {
            using var context = _dbContextFactory.CreateDbContext();
            var memoryStream = new MemoryStream();

            if (entityType == IExcelDataService.SERVER_KEY)
            {

                var commonAssetData = await context.CommonAssets
                    .Include(ca => ca.Server)
                    .Where(ca => ca.Server != null)
                    .Select(ca => new 
                    {
                        ca.Id,
                        ca.ManagementTag,
                        ca.Name,
                        ca.Role,
                        ca.ApplyDateTime,
                        ca.ResponsibleCompany,
                        ca.ResponsiblePerson,
                        ca.ResponsiblePersonPhone,
                        ca.OnSiteManager,
                        ca.OnSiteManagerPhone
                    })
                    .ToListAsync();

                var serverData = await context.Servers
                    .Select(s => new
                    {
                        s.Id,
                        s.CommonAssetId
                    })
                    .ToListAsync();

                // ServerDevice 데이터 생성 (Server Id 참조 포함)
                var serverDeviceData = await context.ServerDevices
                    .Include(sd => sd.Server)
                    .Where(sd => sd.Server != null)
                    .Select(sd => new
                    {
                        sd.Id,
                        sd.Manufacturer,
                        sd.Model,
                        sd.SerialNumber,
                        sd.Ram,
                        sd.Disk,
                        sd.Rack,
                        sd.NetworkType,
                        sd.MountedPhysicalServer,
                        sd.OsType,
                        sd.OsVersion,
                        sd.OsBit,
                        sd.CpuClockGhz,
                        sd.CpuCores,
                        sd.InternalDisk,
                        sd.ExternalDisk,
                        sd.NicCount,
                        sd.HbaCount,
                        sd.IpAddress,
                        sd.UnitSize,
                        sd.Notes,
                        sd.ServerId
                    })
                    .ToListAsync();

                // Server와 관련된 RoutineCheck 데이터
                var serverRoutineChecksData = await context.RoutineChecks
                    .Include(rc => rc.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(rc => rc.CommonAsset.Server != null)
                    .Select(rc => new
                    {
                        rc.Id,
                        rc.Detail,
                        rc.StartDateTime,
                        rc.EndDateTime,
                        rc.CommonAssetId
                    })
                    .ToListAsync();

                // Server와 관련된 SecurityVulnerability 데이터
                var serverSecurityVulnerabilitiesData = await context.SecurityVulnerabilities
                    .Include(sv => sv.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(sv => sv.CommonAsset.Server != null)
                    .Select(sv => new
                    {
                        sv.Id,
                        sv.DiscoveryDateTime,
                        sv.VulnerabilityDetail,
                        sv.VisitDateTime,
                        sv.ResolveDateTime,
                        sv.TaskDetail,
                        sv.IsResolved,
                        sv.Level,
                        sv.CommonAssetId
                    })
                    .ToListAsync();

                // Server와 관련된 Maintenance 데이터
                var serverMaintenancesData = await context.Maintenances
                    .Include(m => m.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(m => m.CommonAsset.Server != null)
                    .Select(m => new
                    {
                        m.Id,
                        m.Description,
                        m.VisitDateTime,
                        m.ResolveDateTime,
                        m.CommonAssetId
                    })
                    .ToListAsync();

                // Server와 관련된 Failure 데이터
                var serverFailuresData = await context.Failures
                    .Include(f => f.CommonAsset)
                    .ThenInclude(ca => ca.Server)
                    .Where(f => f.CommonAsset.Server != null)
                    .Select(f => new
                    {
                        f.Id,
                        f.FailureDateTime,
                        f.Description,
                        f.VisitDateTime,
                        f.ResolveDateTime,
                        f.DisabilityHours,
                        f.ResolveDescription,
                        f.IsResolved,
                        f.CommonAssetId
                    })
                    .ToListAsync();

                // 멀티 시트 엑셀 파일 생성
                var sheets = new Dictionary<string, object>
                {
                    [IExcelDataService.COMMON_ASSET_KEY] = commonAssetData,
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

            // MiniExcel requires a seekable stream; Blazor's RemoteFileEntryStream is not seekable.
            using var memoryStream = new MemoryStream();
            await excelStream.CopyToAsync(memoryStream);

            if (entityType == IExcelDataService.SERVER_KEY)
            {
                // 멀티 시트 엑셀에서 각 시트를 시트명으로 읽어 저장
                List<TSheet> ReadSheet<TSheet>(string sheetName) where TSheet : class, new()
                {
                    memoryStream.Position = 0;
                    return memoryStream.Query<TSheet>(sheetName: sheetName).ToList();
                }

                var commonAssets = ReadSheet<CommonAsset>(IExcelDataService.COMMON_ASSET_KEY);
                var servers = ReadSheet<Server>(IExcelDataService.SERVER_KEY);
                var serverDevices = ReadSheet<ServerDevice>(IExcelDataService.SERVER_DEVICE_KEY);
                var routineChecks = ReadSheet<RoutineCheck>(IExcelDataService.SERVER_ROUTINE_CHECKS_KEY);
                var securityVulns = ReadSheet<SecurityVulnerability>(IExcelDataService.SERVER_SECURITY_VULNERABILITIES_KEY);
                var maintenances = ReadSheet<Maintenance>(IExcelDataService.SERVER_MAINTENANCES_KEY);
                var failures = ReadSheet<Failure>(IExcelDataService.SERVER_FAILURES_KEY);

                // 기존 데이터 삭제 (FK 제약 순서: 자식 → 부모)
                var existingCommonAssetIds = await context.Servers
                    .Select(s => s.CommonAssetId)
                    .ToListAsync();

                await context.Failures
                    .Where(f => existingCommonAssetIds.Contains(f.CommonAssetId))
                    .ExecuteDeleteAsync();
                await context.Maintenances
                    .Where(m => existingCommonAssetIds.Contains(m.CommonAssetId))
                    .ExecuteDeleteAsync();
                await context.SecurityVulnerabilities
                    .Where(sv => existingCommonAssetIds.Contains(sv.CommonAssetId))
                    .ExecuteDeleteAsync();
                await context.RoutineChecks
                    .Where(rc => existingCommonAssetIds.Contains(rc.CommonAssetId))
                    .ExecuteDeleteAsync();
                await context.ServerDevices.ExecuteDeleteAsync();
                await context.Servers.ExecuteDeleteAsync();
                await context.CommonAssets
                    .Where(ca => existingCommonAssetIds.Contains(ca.Id))
                    .ExecuteDeleteAsync();

                // PK 충돌 방지: 구 ID로 매핑 테이블 생성 후 모든 Id를 0으로 초기화
                var caMap = commonAssets.ToDictionary(ca => { var id = ca.Id; ca.Id = 0; return id; }, ca => ca);
                var serverMap = servers.ToDictionary(s =>
                {
                    var id = s.Id;
                    s.Id = 0;
                    if (caMap.TryGetValue(s.CommonAssetId, out var ca)) s.CommonAsset = ca;
                    s.CommonAssetId = 0;
                    return id;
                }, s => s);

                foreach (var sd in serverDevices)
                {
                    var oldServerId = sd.ServerId;
                    sd.Id = 0;
                    sd.ServerId = 0;
                    if (serverMap.TryGetValue(oldServerId, out var server)) sd.Server = server;
                }
                foreach (var rc in routineChecks)
                {
                    var oldCaId = rc.CommonAssetId;
                    rc.Id = 0;
                    rc.CommonAssetId = 0;
                    if (caMap.TryGetValue(oldCaId, out var ca)) rc.CommonAsset = ca;
                }
                foreach (var sv in securityVulns)
                {
                    var oldCaId = sv.CommonAssetId;
                    sv.Id = 0;
                    sv.CommonAssetId = 0;
                    if (caMap.TryGetValue(oldCaId, out var ca)) sv.CommonAsset = ca;
                }
                foreach (var m in maintenances)
                {
                    var oldCaId = m.CommonAssetId;
                    m.Id = 0;
                    m.CommonAssetId = 0;
                    if (caMap.TryGetValue(oldCaId, out var ca)) m.CommonAsset = ca;
                }
                foreach (var f in failures)
                {
                    var oldCaId = f.CommonAssetId;
                    f.Id = 0;
                    f.CommonAssetId = 0;
                    if (caMap.TryGetValue(oldCaId, out var ca)) f.CommonAsset = ca;
                }

                context.CommonAssets.AddRange(commonAssets);
                context.Servers.AddRange(servers);
                context.ServerDevices.AddRange(serverDevices);
                context.RoutineChecks.AddRange(routineChecks);
                context.SecurityVulnerabilities.AddRange(securityVulns);
                context.Maintenances.AddRange(maintenances);
                context.Failures.AddRange(failures);
            }
            else
            {
                var rows = memoryStream.Query<T>().ToList();

                switch (entityType)
                {
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
                    
                    default:
                        throw new ArgumentException($"Unknown entity type: {entityType}");
                }
            }

            return await context.SaveChangesAsync();
        }
    }
}