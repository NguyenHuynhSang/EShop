using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi
{
    public static class Constain
    {
        public static string GET_PROVINCE_URL = @"https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/province";
        public static string GET_DISTRIC_URL = @"https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/district";
        public static string GET_WARD_URL = @"https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/ward";

        public static string GET_AVALABLE_SHIPPING_SERVICE_URL = @"https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/available-services";
        public static string GET__SHIPPING_FREE_URL = @"https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee";

        public static string POST_CREATE_ORDER_URL = @"https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create";




        public static string SHOP_ID = @"76806";
        public static string Token = @"e9d4d68a-4d0f-11eb-aad2-9ec3d9780e97";
        public static string SHOP_DEFAULT_DISTRICT_CODE = @"1451";


    }
}
