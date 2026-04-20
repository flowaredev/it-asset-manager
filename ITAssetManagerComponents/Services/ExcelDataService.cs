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
        const string STORAGE_DEVICE_KEY = "StorageDevice";
        const string NETWORK_EQUIPMENT_KEY = "NetworkEquipment";
        const string NETWORK_DEVICE_KEY = "NetworkDevice";
        const string SECURITY_EQUIPMENT_KEY = "SecurityEquipment";
        const string SECURITY_DEVICE_KEY = "SecurityDevice";
        const string SOFTWARE_KEY = "Software";
        const string SOFTWARE_DEVICE_KEY = "SoftwareDevice";
        const string SUPPORT_EQUIPMENT_KEY = "SupportEquipment";
        const string SUPPORT_DEVICE_KEY = "SupportDevice";
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

            var sheets = new Dictionary<string, object>();

            // CommonAsset 및 관련 활동 데이터를 공통으로 조회하는 헬퍼
            async Task AddCommonAssetSheetsAsync(
                IQueryable<CommonAsset> commonAssetQuery,
                List<int> commonAssetIds,
                string routineChecksKey,
                string securityVulnerabilitiesKey,
                string maintenancesKey,
                string failuresKey)
            {
                sheets[IExcelDataService.COMMON_ASSET_KEY] = await commonAssetQuery
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

                sheets[routineChecksKey] = await context.RoutineChecks
                    .Where(rc => commonAssetIds.Contains(rc.CommonAssetId))
                    .Select(rc => new
                    {
                        rc.Id,
                        rc.Detail,
                        rc.StartDateTime,
                        rc.EndDateTime,
                        rc.CommonAssetId
                    })
                    .ToListAsync();

                sheets[securityVulnerabilitiesKey] = await context.SecurityVulnerabilities
                    .Where(sv => commonAssetIds.Contains(sv.CommonAssetId))
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

                sheets[maintenancesKey] = await context.Maintenances
                    .Where(m => commonAssetIds.Contains(m.CommonAssetId))
                    .Select(m => new
                    {
                        m.Id,
                        m.Description,
                        m.VisitDateTime,
                        m.ResolveDateTime,
                        m.CommonAssetId
                    })
                    .ToListAsync();

                sheets[failuresKey] = await context.Failures
                    .Where(f => commonAssetIds.Contains(f.CommonAssetId))
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
            }

            switch (entityType)
            {
                case IExcelDataService.SERVER_KEY:
                {
                    var commonAssetIds = await context.Servers.Select(s => s.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        IExcelDataService.SERVER_ROUTINE_CHECKS_KEY,
                        IExcelDataService.SERVER_SECURITY_VULNERABILITIES_KEY,
                        IExcelDataService.SERVER_MAINTENANCES_KEY,
                        IExcelDataService.SERVER_FAILURES_KEY);

                    sheets[IExcelDataService.SERVER_KEY] = await context.Servers
                        .Select(s => new { s.Id, s.CommonAssetId })
                        .ToListAsync();

                    sheets[IExcelDataService.SERVER_DEVICE_KEY] = await context.ServerDevices
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
                    break;
                }
                case IExcelDataService.STORAGE_KEY:
                {
                    var commonAssetIds = await context.Storages.Select(s => s.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        "RoutineChecks",
                        "SecurityVulnerabilities",
                        "Maintenances",
                        "Failures");

                    sheets[IExcelDataService.STORAGE_KEY] = await context.Storages
                        .Select(s => new { s.Id, s.CommonAssetId })
                        .ToListAsync();

                    sheets[IExcelDataService.STORAGE_DEVICE_KEY] = await context.StorageDevices
                        .Where(sd => sd.Storage != null)
                        .Select(sd => new
                        {
                            sd.Id,
                            sd.Manufacturer,
                            sd.Model,
                            sd.SerialNumber,
                            sd.PhysicalDiskInfo,
                            sd.DiskBackupInfo,
                            sd.IpAddress,
                            sd.Rack,
                            sd.UnitSize,
                            sd.Notes,
                            sd.StorageId
                        })
                        .ToListAsync();
                    break;
                }
                case IExcelDataService.NETWORK_EQUIPMENT_KEY:
                {
                    var commonAssetIds = await context.NetworkEquipments.Select(n => n.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        "RoutineChecks",
                        "SecurityVulnerabilities",
                        "Maintenances",
                        "Failures");

                    sheets[IExcelDataService.NETWORK_EQUIPMENT_KEY] = await context.NetworkEquipments
                        .Select(n => new { n.Id, n.CommonAssetId })
                        .ToListAsync();

                    sheets[IExcelDataService.NETWORK_DEVICE_KEY] = await context.NetworkDevices
                        .Where(nd => nd.NetworkEquipment != null)
                        .Select(nd => new
                        {
                            nd.Id,
                            nd.Manufacturer,
                            nd.Model,
                            nd.SerialNumber,
                            nd.OsVersion,
                            nd.MainMemory,
                            nd.FlashMemory,
                            nd.IpAddress,
                            nd.Rack,
                            nd.UnitSize,
                            nd.Notes,
                            nd.NetworkEquipmentId
                        })
                        .ToListAsync();
                    break;
                }
                case IExcelDataService.SECURITY_EQUIPMENT_KEY:
                {
                    var commonAssetIds = await context.SecurityEquipments.Select(s => s.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        "RoutineChecks",
                        "SecurityVulnerabilities",
                        "Maintenances",
                        "Failures");

                    sheets[IExcelDataService.SECURITY_EQUIPMENT_KEY] = await context.SecurityEquipments
                        .Select(s => new { s.Id, s.CommonAssetId })
                        .ToListAsync();

                    sheets[IExcelDataService.SECURITY_DEVICE_KEY] = await context.SecurityDevices
                        .Where(sd => sd.SecurityEquipment != null)
                        .Select(sd => new
                        {
                            sd.Id,
                            sd.Manufacturer,
                            sd.Model,
                            sd.SerialNumber,
                            sd.DeviceSpec,
                            sd.IpAddress,
                            sd.Rack,
                            sd.UnitSize,
                            sd.Notes,
                            sd.SecurityEquipmentId
                        })
                        .ToListAsync();
                    break;
                }
                case IExcelDataService.SOFTWARE_KEY:
                {
                    var commonAssetIds = await context.Softwares.Select(s => s.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        "RoutineChecks",
                        "SecurityVulnerabilities",
                        "Maintenances",
                        "Failures");

                    sheets[IExcelDataService.SOFTWARE_KEY] = await context.Softwares
                        .Select(s => new { s.Id, s.CommonAssetId })
                        .ToListAsync();

                    sheets[IExcelDataService.SOFTWARE_DEVICE_KEY] = await context.SoftwareDevices
                        .Where(sd => sd.Software != null)
                        .Select(sd => new
                        {
                            sd.Id,
                            sd.Manufacturer,
                            sd.ProgramName,
                            sd.SerialNumber,
                            sd.Notes,
                            sd.SoftwareId
                        })
                        .ToListAsync();
                    break;
                }
                case IExcelDataService.SUPPORT_EQUIPMENT_KEY:
                {
                    var commonAssetIds = await context.SupportEquipments.Select(s => s.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        "RoutineChecks",
                        "SecurityVulnerabilities",
                        "Maintenances",
                        "Failures");

                    sheets[IExcelDataService.SUPPORT_EQUIPMENT_KEY] = await context.SupportEquipments
                        .Select(s => new { s.Id, s.CommonAssetId })
                        .ToListAsync();

                    sheets[IExcelDataService.SUPPORT_DEVICE_KEY] = await context.SupportDevices
                        .Where(sd => sd.SupportEquipment != null)
                        .Select(sd => new
                        {
                            sd.Id,
                            sd.Manufacturer,
                            sd.Model,
                            sd.SerialNumber,
                            sd.DeviceSpec,
                            sd.IpAddress,
                            sd.Location,
                            sd.Notes,
                            sd.SupportEquipmentId
                        })
                        .ToListAsync();
                    break;
                }
                case IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY:
                {
                    var commonAssetIds = await context.MiscellaneousEquipments.Select(m => m.CommonAssetId).ToListAsync();
                    var commonAssetQuery = context.CommonAssets.Where(ca => commonAssetIds.Contains(ca.Id));

                    await AddCommonAssetSheetsAsync(
                        commonAssetQuery, commonAssetIds,
                        "RoutineChecks",
                        "SecurityVulnerabilities",
                        "Maintenances",
                        "Failures");

                    sheets[IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY] = await context.MiscellaneousEquipments
                        .Select(m => new { m.Id, m.CommonAssetId })
                        .ToListAsync();
                    break;
                }
                default:
                    throw new ArgumentException($"Unknown entity type: {entityType}");
            }

            await memoryStream.SaveAsAsync(sheets);

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