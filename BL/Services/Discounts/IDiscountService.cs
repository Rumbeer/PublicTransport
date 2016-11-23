using BL.DTOs.Discounts;
using System.Collections.Generic;
using BL.Enum;

namespace BL.Services.Discounts
{
    public interface IDiscountService
    {
        /// <summary>
        /// Created new Discount
        /// </summary>
        /// <param name="discountDto">discount details</param>
        /// <param name="companyId">id of company</param>
        void CreateDiscount(DiscountDTO discountDto, int companyId);

        /// <summary>
        /// Updates discount
        /// </summary>
        /// <param name="discountDto">discount details</param>
        void EditDiscount(DiscountDTO discountDto);

        /// <summary>
        /// Deletes discount
        /// </summary>
        /// <param name="discountId">id of dicount</param>
        void DeleteDiscount(int discountId);

        /// <summary>
        /// Gets discount of specific id
        /// </summary>
        /// <param name="discountId">id of discount</param>
        /// <returns></returns>
        DiscountDTO GetDiscountById(int discountId);

        /// <summary>
        /// Gets discounts of specific company
        /// </summary>
        /// <param name="companyId">id of company</param>
        /// <param name="discountType">type of discount</param>
        /// <returns></returns>
        IEnumerable<DiscountDTO> ListDiscountsOfCompany(DiscountType? discountType, int companyId);
    }
}
