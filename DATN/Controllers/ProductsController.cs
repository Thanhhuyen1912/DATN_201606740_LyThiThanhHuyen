using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SANPHAM.Controllers
{
    
    public class ProductsController : Controller
    {

        //public const string AllRoles = CConfig.Admin + "," + CConfig.QuanlySP + "," + CConfig.Quanly + "," + CConfig.KhachHang;
        //public const string Admin = CConfig.Admin;
        //public const string QL = CConfig.Admin + "," + CConfig.QuanlySP + "," + CConfig.Quanly;
        ////[RequiredLogin]
        ////[AuthorizeMethod(AllRoles)]
        [HttpGet]
        public IActionResult Index()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            //{
            //    // Chuyển hướng đến trang đăng nhập
            //    return RedirectToAction("Index", "Account");
            //}
            return View();
        }
        ////[RequiredLogin]
        ////[AuthorizeMethod(AllRoles)]
        //[HttpGet]
        //public IActionResult IndexKAPI()
        //{
        //    //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
        //    //{

        //    //    // Chuyển hướng đến trang đăng nhập
        //    //    return RedirectToAction("Index", "Account");
        //    //}
        //    return View();
        //}

        //[RequiredLogin]
        //[AuthorizeMethod(QL)]
        //[HttpPost]
        //public async Task<IActionResult> AddProduct([FromBody] PRODUCTS product)
        //{

        //    try
        //    {
        //        // Kiểm tra dữ liệu đầu vào (validation phía server)
        //        if (string.IsNullOrWhiteSpace(product.PRODUCT_NAME) ||
        //            product.PRODUCT_ID < 0 ||
        //            product.CATEGORY_ID == null ||
        //            product.PRICE == null ||
        //            product.QUANTITY == null)
        //        {
        //            return BadRequest(new { code = 400, message = "Thông tin chưa phù hợp." });
        //        }

        //        // Gọi API để thêm sản phẩm
        //        var result = await CCallApi.PostTemplateAsync(CConfig.API_INSERT_PRODUCT, product);


        //        // Kiểm tra kết quả trả về từ API
        //        if (result != null)
        //        {
        //            // Giả sử kết quả trả về là một đối tượng JSON với các thuộc tính "code" và "message"
        //            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result.ToString());

        //            if (apiResponse != null)
        //            {
        //                if (apiResponse.Code == 0)
        //                {
        //                    // Thành công
        //                    return Ok(new
        //                    {
        //                        code = 0,
        //                        message = "Sản phẩm đã được thêm thành công.",
        //                        data = apiResponse.Data
        //                    });
        //                }
        //                else
        //                {
        //                    // Thất bại, trả về lỗi từ API
        //                    return BadRequest(new
        //                    {
        //                        code = apiResponse.Code,
        //                        message = apiResponse.Message
        //                    });
        //                }
        //            }
        //        }

        //        // Nếu kết quả trả về null hoặc không hợp lệ
        //        return StatusCode(500, new { code = 500, message = "Không thể thêm sản phẩm. Dữ liệu trả về không hợp lệ." });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log lỗi và trả về lỗi server
        //        return StatusCode(500, new { code = 500, message = "Lỗi khi xử lý yêu cầu." });
        //    }
        //}
        //[RequiredLogin]
        //[AuthorizeMethod(QL)]
        //[HttpPut]
        //public async Task<IActionResult> UpdateProduct([FromBody] PRODUCTS product)
        //{
        //    try
        //    {
        //        // Kiểm tra dữ liệu đầu vào (validation phía server)
        //        if (string.IsNullOrWhiteSpace(product.PRODUCT_NAME) ||
        //            product.PRODUCT_ID <= 0 ||
        //            product.CATEGORY_ID == null ||
        //            product.PRICE == null ||
        //            product.QUANTITY == null)
        //        {
        //            return BadRequest(new { code = 400, message = "Thông tin chưa phù hợp." });
        //        }

        //        // Gọi API để cập nhật sản phẩm
        //        var result = await CCallApi.UpdateTemplateAsync(CConfig.API_UPDATE_PRODUCT, product);             

        //        // Kiểm tra kết quả trả về từ API
        //        if (result != null)
        //        {
        //            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result.ToString());

        //            if (apiResponse != null)
        //            {
        //                if (apiResponse.Code == 0)
        //                {
        //                    // Thành công
        //                    return Ok(new
        //                    {
        //                        code = 0,
        //                        message = "Sản phẩm đã được cập nhật thành công.",
        //                        data = apiResponse.Data
        //                    });
        //                }
        //                else
        //                {
        //                    // Thất bại, trả về lỗi từ API
        //                    return BadRequest(new
        //                    {
        //                        code = apiResponse.Code,
        //                        message = apiResponse.Message
        //                    });
        //                }
        //            }
        //        }

        //        // Nếu kết quả trả về null hoặc không hợp lệ
        //        return StatusCode(500, new { code = 500, message = "Không thể cập nhật sản phẩm. Dữ liệu trả về không hợp lệ." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { code = 500, message = "Lỗi khi xử lý yêu cầu." });
        //    }
        //}
        ////[RequiredLogin]
        ////[AuthorizeMethod(AllRoles)]
        //[HttpGet]
        //public async Task<IActionResult> SearchProducts(string? productName, int? categoryId, decimal? price, int? quantity)
        //{
        //    try
        //    {
        //        // Gọi phương thức SearchTemplateAsync để lấy dữ liệu từ API
        //        var result = await CCallApi.SearchTemplateAsync(
        //            CConfig.API_SEARCH_GETPRODUCTS,
        //            productName,
        //            categoryId,
        //            price,
        //            quantity
        //        );

        //        if (result != null)
        //        {
        //            // Chuyển đổi dữ liệu thành JSON và trả về phản hồi
        //            return Content(JsonConvert.SerializeObject(result), "application/json");
        //        }
        //        else
        //        {
        //            // Trả về mã lỗi 404 nếu không tìm thấy dữ liệu
        //            return NotFound(new { code = 404, message = "No products found with the specified criteria." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { code = 500, message = "An error occurred while processing your request." });
        //    }
        //}

        ////[RequiredLogin]
        ////[AuthorizeMethod(AllRoles)]
        //[HttpGet]
        //public async Task<IActionResult> GetProductList()
        //{
        //    try
        //    {
        //        // Use the CCallApi's GetProductListAsync method to fetch data
        //        var result = await CCallApi.GetProductListAsync(CConfig.API_GET_PRODUCTS);

        //        if (result != null)
        //        {
        //            // Convert the result to JSON for returning in the response
        //            return Content(JsonConvert.SerializeObject(result), "application/json");
        //        }
        //        else
        //        {
        //            // Return a 500 status code if the data could not be retrieved
        //            return StatusCode(500, "Error loading product data from API.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}
        //[RequiredLogin]
        //[AuthorizeMethod(Admin)]
        //[HttpDelete]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    // Kiểm tra ID hợp lệ
        //    if (id <= 0)
        //        return BadRequest(new { code = 400, message = "ID không hợp lệ." });
        //    try
        //    {
        //        // Gọi API để xóa sản phẩm
        //        var result = await CCallApi.DeleteTemplateAsync(CConfig.API_DELETE_PRODUCT, id);

        //        // Kiểm tra kết quả trả về từ API
        //        if (result != null)
        //        {
        //            // Deserialize kết quả để kiểm tra chi tiết
        //            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result.ToString());

        //            if (apiResponse != null)
        //            {
        //                if (apiResponse.Code == 0) // Thành công
        //                {
        //                    return Ok(new
        //                    {
        //                        code = apiResponse.Code,
        //                        message = "Xóa sản phẩm thành công."
        //                    });
        //                }
        //                else
        //                {
        //                    return BadRequest(new
        //                    {
        //                        code = apiResponse.Code,
        //                        message = apiResponse.Message ?? "Không thể xóa sản phẩm."
        //                    });
        //                }
        //            }
        //        }

        //        // Nếu kết quả trả về null hoặc không hợp lệ
        //        return StatusCode(500, new { code = 500, message = "Không thể xóa sản phẩm. Kết quả trả về không hợp lệ." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { code = 500, message = "Đã xảy ra lỗi khi xóa sản phẩm." });
        //    }
        //}

        ////[RequiredLogin]
        ////[AuthorizeMethod(QL)]
        //[HttpGet]
        //public async Task<IActionResult> GetProductDetails(int id)
        //{
        //    //try
        //    //{
        //    //    // Kiểm tra nếu ID không hợp lệ
        //    //    if (id <= 0)
        //    //    {
        //    //        return BadRequest(new { code = 400, message = "ID sản phẩm không hợp lệ." });
        //    //    }

        //    //    // Gọi phương thức GetProductDetailsAsync để lấy dữ liệu
        //    //   // var result = await CCallApi.GetProductDetailsAsync(CConfig.API_GET_PRODUCT, id);

        //    //    if (result != null)
        //    //    {
        //    //        // Convert dữ liệu sang JSON và trả về phản hồi
        //    //        return Content(JsonConvert.SerializeObject(result), "application/json");
        //    //    }
        //    //    else
        //    //    {
        //    //        // Trả về mã lỗi 500 nếu không thể lấy dữ liệu
        //    //        return StatusCode(500, "Error loading product details from API.");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return StatusCode(500, "An error occurred while processing your request.");
        //    //}
        //    return null;
        //}


    }
}
