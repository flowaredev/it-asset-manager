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
        const string ROUTINE_CHECKS_KEY = "RoutineChecks";
        const string SECURITY_VULNERABILITIES_KEY = "SecurityVulnerabilities";
        const string MAINTENANCES_KEY = "Maintenances";
        const string FAILURES_KEY = "Failures";

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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
                            IExcelDataService.ROUTINE_CHECKS_KEY,
                            IExcelDataService.SECURITY_VULNERABILITIES_KEY,
                            IExcelDataService.MAINTENANCES_KEY,
                            IExcelDataService.FAILURES_KEY);

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

            // 멀티 시트 엑셀에서 각 시트를 시트명으로 읽어 저장
            List<TSheet> ReadSheet<TSheet>(string sheetName) where TSheet : class, new()
            {
                memoryStream.Position = 0;
                return memoryStream.Query<TSheet>(sheetName: sheetName).ToList();
            }

            // 공통 시트 읽기
            var commonAssets = ReadSheet<CommonAsset>(IExcelDataService.COMMON_ASSET_KEY);
            var routineChecks = ReadSheet<RoutineCheck>(IExcelDataService.ROUTINE_CHECKS_KEY);
            var securityVulns = ReadSheet<SecurityVulnerability>(IExcelDataService.SECURITY_VULNERABILITIES_KEY);
            var maintenances = ReadSheet<Maintenance>(IExcelDataService.MAINTENANCES_KEY);
            var failures = ReadSheet<Failure>(IExcelDataService.FAILURES_KEY);

            // 기존 데이터의 CommonAssetId 목록 조회
            List<int> existingCommonAssetIds;
            switch (entityType)
            {
                case IExcelDataService.SERVER_KEY:
                    existingCommonAssetIds = await context.Servers.Select(s => s.CommonAssetId).ToListAsync();
                    break;
                case IExcelDataService.STORAGE_KEY:
                    existingCommonAssetIds = await context.Storages.Select(s => s.CommonAssetId).ToListAsync();
                    break;
                case IExcelDataService.NETWORK_EQUIPMENT_KEY:
                    existingCommonAssetIds = await context.NetworkEquipments.Select(n => n.CommonAssetId).ToListAsync();
                    break;
                case IExcelDataService.SECURITY_EQUIPMENT_KEY:
                    existingCommonAssetIds = await context.SecurityEquipments.Select(s => s.CommonAssetId).ToListAsync();
                    break;
                case IExcelDataService.SOFTWARE_KEY:
                    existingCommonAssetIds = await context.Softwares.Select(s => s.CommonAssetId).ToListAsync();
                    break;
                case IExcelDataService.SUPPORT_EQUIPMENT_KEY:
                    existingCommonAssetIds = await context.SupportEquipments.Select(s => s.CommonAssetId).ToListAsync();
                    break;
                case IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY:
                    existingCommonAssetIds = await context.MiscellaneousEquipments.Select(m => m.CommonAssetId).ToListAsync();
                    break;
                default:
                    throw new ArgumentException($"Unknown entity type: {entityType}");
            }

            // 기존 데이터 삭제 (FK 제약 순서: 활동 → 디바이스 → 장비 → CommonAsset)
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

            switch (entityType)
            {
                case IExcelDataService.SERVER_KEY:
                    await context.ServerDevices.ExecuteDeleteAsync();
                    await context.Servers.ExecuteDeleteAsync();
                    break;
                case IExcelDataService.STORAGE_KEY:
                    await context.StorageDevices.ExecuteDeleteAsync();
                    await context.Storages.ExecuteDeleteAsync();
                    break;
                case IExcelDataService.NETWORK_EQUIPMENT_KEY:
                    await context.NetworkDevices.ExecuteDeleteAsync();
                    await context.NetworkEquipments.ExecuteDeleteAsync();
                    break;
                case IExcelDataService.SECURITY_EQUIPMENT_KEY:
                    await context.SecurityDevices.ExecuteDeleteAsync();
                    await context.SecurityEquipments.ExecuteDeleteAsync();
                    break;
                case IExcelDataService.SOFTWARE_KEY:
                    await context.SoftwareDevices.ExecuteDeleteAsync();
                    await context.Softwares.ExecuteDeleteAsync();
                    break;
                case IExcelDataService.SUPPORT_EQUIPMENT_KEY:
                    await context.SupportDevices.ExecuteDeleteAsync();
                    await context.SupportEquipments.ExecuteDeleteAsync();
                    break;
                case IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY:
                    await context.MiscellaneousEquipments.ExecuteDeleteAsync();
                    break;
            }

            await context.CommonAssets
                .Where(ca => existingCommonAssetIds.Contains(ca.Id))
                .ExecuteDeleteAsync();

            // PK 충돌 방지: 구 ID로 매핑 테이블 생성 후 모든 Id를 0으로 초기화
            var caMap = commonAssets.ToDictionary(ca => { var id = ca.Id; ca.Id = 0; return id; }, ca => ca);

            // 장비 + 디바이스 시트 읽기 및 ID 리매핑
            switch (entityType)
            {
                case IExcelDataService.SERVER_KEY:
                    {
                        var servers = ReadSheet<Server>(IExcelDataService.SERVER_KEY);
                        var serverDevices = ReadSheet<ServerDevice>(IExcelDataService.SERVER_DEVICE_KEY);

                        // MiniExcel이 빈 숫자 셀을 NaN으로 읽는 경우 null로 변환 (MySQL은 NaN 미지원)
                        foreach (var sd in serverDevices)
                        {
                            if (sd.Ram is double ram && double.IsNaN(ram)) sd.Ram = null;
                            if (sd.CpuClockGhz is double ghz && double.IsNaN(ghz)) sd.CpuClockGhz = null;
                        }

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
                            var oldId = sd.ServerId;
                            sd.Id = 0;
                            sd.ServerId = 0;
                            if (serverMap.TryGetValue(oldId, out var server)) sd.Server = server;
                        }

                        context.Servers.AddRange(servers);
                        context.ServerDevices.AddRange(serverDevices);
                        break;
                    }
                case IExcelDataService.STORAGE_KEY:
                    {
                        var storages = ReadSheet<Storage>(IExcelDataService.STORAGE_KEY);
                        var storageDevices = ReadSheet<StorageDevice>(IExcelDataService.STORAGE_DEVICE_KEY);

                        var storageMap = storages.ToDictionary(s =>
                        {
                            var id = s.Id;
                            s.Id = 0;
                            if (caMap.TryGetValue(s.CommonAssetId, out var ca)) s.CommonAsset = ca;
                            s.CommonAssetId = 0;
                            return id;
                        }, s => s);

                        foreach (var sd in storageDevices)
                        {
                            var oldId = sd.StorageId;
                            sd.Id = 0;
                            sd.StorageId = 0;
                            if (storageMap.TryGetValue(oldId, out var storage)) sd.Storage = storage;
                        }

                        context.Storages.AddRange(storages);
                        context.StorageDevices.AddRange(storageDevices);
                        break;
                    }
                case IExcelDataService.NETWORK_EQUIPMENT_KEY:
                    {
                        var networkEquipments = ReadSheet<NetworkEquipment>(IExcelDataService.NETWORK_EQUIPMENT_KEY);
                        var networkDevices = ReadSheet<NetworkDevice>(IExcelDataService.NETWORK_DEVICE_KEY);

                        var equipmentMap = networkEquipments.ToDictionary(n =>
                        {
                            var id = n.Id;
                            n.Id = 0;
                            if (caMap.TryGetValue(n.CommonAssetId, out var ca)) n.CommonAsset = ca;
                            n.CommonAssetId = 0;
                            return id;
                        }, n => n);

                        foreach (var nd in networkDevices)
                        {
                            var oldId = nd.NetworkEquipmentId;
                            nd.Id = 0;
                            nd.NetworkEquipmentId = 0;
                            if (equipmentMap.TryGetValue(oldId, out var equipment)) nd.NetworkEquipment = equipment;
                        }

                        context.NetworkEquipments.AddRange(networkEquipments);
                        context.NetworkDevices.AddRange(networkDevices);
                        break;
                    }
                case IExcelDataService.SECURITY_EQUIPMENT_KEY:
                    {
                        var securityEquipments = ReadSheet<SecurityEquipment>(IExcelDataService.SECURITY_EQUIPMENT_KEY);
                        var securityDevices = ReadSheet<SecurityDevice>(IExcelDataService.SECURITY_DEVICE_KEY);

                        var equipmentMap = securityEquipments.ToDictionary(s =>
                        {
                            var id = s.Id;
                            s.Id = 0;
                            if (caMap.TryGetValue(s.CommonAssetId, out var ca)) s.CommonAsset = ca;
                            s.CommonAssetId = 0;
                            return id;
                        }, s => s);

                        foreach (var sd in securityDevices)
                        {
                            var oldId = sd.SecurityEquipmentId;
                            sd.Id = 0;
                            sd.SecurityEquipmentId = 0;
                            if (equipmentMap.TryGetValue(oldId, out var equipment)) sd.SecurityEquipment = equipment;
                        }

                        context.SecurityEquipments.AddRange(securityEquipments);
                        context.SecurityDevices.AddRange(securityDevices);
                        break;
                    }
                case IExcelDataService.SOFTWARE_KEY:
                    {
                        var softwares = ReadSheet<Software>(IExcelDataService.SOFTWARE_KEY);
                        var softwareDevices = ReadSheet<SoftwareDevice>(IExcelDataService.SOFTWARE_DEVICE_KEY);

                        var softwareMap = softwares.ToDictionary(s =>
                        {
                            var id = s.Id;
                            s.Id = 0;
                            if (caMap.TryGetValue(s.CommonAssetId, out var ca)) s.CommonAsset = ca;
                            s.CommonAssetId = 0;
                            return id;
                        }, s => s);

                        foreach (var sd in softwareDevices)
                        {
                            var oldId = sd.SoftwareId;
                            sd.Id = 0;
                            sd.SoftwareId = 0;
                            if (softwareMap.TryGetValue(oldId, out var software)) sd.Software = software;
                        }

                        context.Softwares.AddRange(softwares);
                        context.SoftwareDevices.AddRange(softwareDevices);
                        break;
                    }
                case IExcelDataService.SUPPORT_EQUIPMENT_KEY:
                    {
                        var supportEquipments = ReadSheet<SupportEquipment>(IExcelDataService.SUPPORT_EQUIPMENT_KEY);
                        var supportDevices = ReadSheet<SupportDevice>(IExcelDataService.SUPPORT_DEVICE_KEY);

                        var equipmentMap = supportEquipments.ToDictionary(s =>
                        {
                            var id = s.Id;
                            s.Id = 0;
                            if (caMap.TryGetValue(s.CommonAssetId, out var ca)) s.CommonAsset = ca;
                            s.CommonAssetId = 0;
                            return id;
                        }, s => s);

                        foreach (var sd in supportDevices)
                        {
                            var oldId = sd.SupportEquipmentId;
                            sd.Id = 0;
                            sd.SupportEquipmentId = 0;
                            if (equipmentMap.TryGetValue(oldId, out var equipment)) sd.SupportEquipment = equipment;
                        }

                        context.SupportEquipments.AddRange(supportEquipments);
                        context.SupportDevices.AddRange(supportDevices);
                        break;
                    }
                case IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY:
                    {
                        var miscEquipments = ReadSheet<MiscellaneousEquipment>(IExcelDataService.MISCELLANEOUS_EQUIPMENT_KEY);

                        foreach (var m in miscEquipments)
                        {
                            var oldCaId = m.CommonAssetId;
                            m.Id = 0;
                            m.CommonAssetId = 0;
                            if (caMap.TryGetValue(oldCaId, out var ca)) m.CommonAsset = ca;
                        }

                        context.MiscellaneousEquipments.AddRange(miscEquipments);
                        break;
                    }
            }

            // 공통 활동 데이터 ID 리매핑
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
            context.RoutineChecks.AddRange(routineChecks);
            context.SecurityVulnerabilities.AddRange(securityVulns);
            context.Maintenances.AddRange(maintenances);
            context.Failures.AddRange(failures);

            return await context.SaveChangesAsync();
        }
    }
}