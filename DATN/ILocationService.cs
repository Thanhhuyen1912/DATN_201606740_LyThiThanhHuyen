namespace DATN
{
    public interface ILocationService
    {
        Task<string> GetProvinceNameByIdAsync(int provinceId);
        Task<string> GetDistrictNameByIdAsync(int districtId);
        Task<string> GetWardNameByIdAsync(int wardId, int districtId);
    }
}
