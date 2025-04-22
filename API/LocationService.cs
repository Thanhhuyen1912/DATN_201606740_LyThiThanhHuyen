using Newtonsoft.Json;
namespace API.Service;
public class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetProvinceNameByIdAsync(int provinceId)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://provinces.open-api.vn/api/p/{provinceId}");
            var province = JsonConvert.DeserializeObject<ProvinceResponse>(response);
            return province?.Name ?? "Không tìm thấy";
        }
        catch
        {
            return "Lỗi khi gọi API";
        }
    }

    public async Task<string> GetDistrictNameByIdAsync(int districtId)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://provinces.open-api.vn/api/d/{districtId}");
            var district = JsonConvert.DeserializeObject<DistrictResponse>(response);
            return district?.Name ?? "Không tìm thấy";
        }
        catch
        {
            return "Lỗi khi gọi API";
        }
    }

    public async Task<string> GetWardNameByIdAsync(int wardId, int districtId)
    {
        try
        {
            // Gọi API lấy thông tin huyện
            var response = await _httpClient.GetStringAsync($"https://provinces.open-api.vn/api/d/{districtId}?depth=2");
            var district = JsonConvert.DeserializeObject<DistrictResponse>(response);

            // Tìm xã trong danh sách Wards
            var ward = district?.Wards?.FirstOrDefault(w => w.Code == wardId);
            return ward?.Name ?? "Không tìm thấy xã";
        }
        catch (Exception ex)
        {
            return $"Lỗi khi gọi API: {ex.Message}";
        }
    }

    public class ProvinceResponse
    {
        public string Name { get; set; }
        public List<District> Districts { get; set; }
    }

    public class DistrictResponse
    {
        public string Name { get; set; }
        public List<Ward> Wards { get; set; }
    }

    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Ward
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
