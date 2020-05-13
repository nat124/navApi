using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Data.Common;
using AllModels;

namespace Localdb
{
    public class PistisContext: DbContext
    {
        public PistisContext(DbContextOptions<PistisContext> options) : base(options)
        {
        }

        public DbSet<HomeCategory> HomeCategory { get; set; }
        public DbSet<HomeCategoryProduct> HomeCategoryProduct { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ProductVariantDetail> ProductVariantDetails { get; set; }
        public DbSet<ProductVariantOption> ProductVariantOptions { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<VariantOption> VariantOptions { get; set; }
        public DbSet<CategoryVariant> CategoryVariants { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<FooterUrl> FooterUrls { get; set; }
        public DbSet<FooterHeader> FooterHeaders { get; set; }
        public DbSet<FeatureProduct> FeatureProducts { get; set; }
        public DbSet<BannerImages> BannerImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        //public DbSet<CheckoutLog> CheckoutLog { get; set; }
        public DbSet<CustomerHistory> CustomerHistories { get; set; }
        public DbSet<FooterUrlData> FooterUrlDatas { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<CustomerGroupUsers> CustomerGroupUsers { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<ProductionSpecification> ProductionSpecifications { get; set; }
        public DbSet<RatingReview> RatingReviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<CompareProduct> CompareProducts { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutItem> CheckoutItems { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<ShippingConfig> ShippingConfig { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<Deal> Deal { get; set; } 
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }
        public DbSet<ReviewStatus> ReviewStatus { get; set; }
        public DbSet<BillingAddress> BillingAddress { get; set; }
        public DbSet<TrackOrder> TrackOrders { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PaymentModes> PaymentModes { get; set; }
        public DbSet<Return> Return { get; set; }
        public DbSet<ReturnImage> ReturnImage { get; set; }
        public DbSet<ReturnStatus> ReturnStatus { get; set; }

        public DbSet<DealProduct> DealProduct { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public DbSet<ProductTag> ProductTag { get; set; }

        public DbSet<SearchTerm> SearchTerm { get; set; }

        public DbSet<PostalCodeMap> PostalCodeMap { get; set; }
        public DbSet<NotificationUser> NotificationUser { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<SpinnerPromotion> SpinnerPromotion { get; set; }
        public DbSet<Mood> Mood { get; set; }
        public DbSet<SpinnerOptionsPeriod> SpinnerOptionsPeriod { get; set; }
        public DbSet<NewsletterImage> NewsletterImage { get; set; }
        public DbSet<ProductCategoryCommission> ProductCategoryCommission { get; set; }

        public DbSet<VendorIDProof> VendorIDProof { get; set; }
        public DbSet<SpinUserData> SpinUserData { get; set; }
        public DbSet<VendorTransaction> VendorTransaction { get; set; }
        public DbSet<VendorTransactionSummary> VendorTransactionSummary { get; set; }
        public DbSet<VendorDocuments> VendorDocuments { get; set; }
        public DbSet<LogControl> LogControl { get; set; }
        public DbSet<RelatedTag> RelatedTag { get; set; }
        public DbSet<ProductRelatedTagMap> ProductRelatedTagMap { get; set; }
        public DbSet<VendorChat> VendorChat { get; set; }
        public DbSet<VendorChatMsg> VendorChatMsg { get; set; }

        public DbSet<PaymentConfiguration> PaymentConfiguration { get; set; }

    }
}
