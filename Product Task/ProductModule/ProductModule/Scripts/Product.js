//function SaveProduct() {

//    alert("SaveProduct");
//    $(document).ready(function () {

//        $(".Save").click(function () {
//            debugger;

//            var ItemName = $("#ItemName").val();
//            var Description = $("#Description").val();
//            var Price = $("#Price").val();
//            var Quantity = $("#Quantity").val();
//            var Tax = $("#Tax").val();

//            $.post("/Product/SaveProduct", $.param([{ name: "ItemName", value: ItemName }, { name: "Description", value: Description }, { name: "Price", value: Price }, { name: "Quantity", value: Quantity }, { name: "Tax", value: Tax}]), function (Data) {

//                if (Data == 'Successfuly Saved') {
//                   
//                    alert("Saved Successfully");
//                    SaveProduct();
//                } else {

//                    alert("Failed to Save");
//                }



//            });
//        });


//    });

//}

//function ItemDtls() {

//    $(document).ready(function () {

//        $.post("/Product/ProductDetails", function (Result) {


//            var ItemDtls = '';
//            ItemDtls = '<thead><tr><th>SL NO</th><th>Item Name</th><th>Description</th><th>Price</th><th>Quantity</th><th>Tax</th> </thead>'

//            $.each(Result, function (index, List) {

//                ItemDtls += '<tbody><tr class="product_row"><td class="Item_Id">' + List.ItemId + '</td><td class="Item_Name">' + List.ItemName + '</td><td class="ClientName">' + List.Descriptions + '</td><td class="Price">' + List.Price + '</td><td class="Qty">' + List.Quantity + '</td><td class="tax">' + List.Tax + '</td></tr></tbody>';

//            });
//            $("#product_id").html(ItemDtls);
//            //$("#product_id").show();
//            $('#product_id').DataTable({
//                responsive: true
//            });
//        });


//    });
//}