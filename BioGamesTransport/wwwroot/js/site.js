// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('#dataTables-orders').DataTable({
        "pageLength": 25,
        responsive: true
    });

    $('#dataTables-customer').DataTable({
        "pageLength": 50,
        responsive: true
    });


    $("#first_step").click(function (event ) {
        $("#li1Step").removeClass("active");
        $("#li2Step").addClass("active");
        $("#li3Step").removeClass("active");
    });

    $("#sec_step_back").click(function () {
        $("#li1Step").addClass("active");
        $("#li2Step").removeClass("active");
        $("#li3Step").removeClass("active");
    });

    $("#third_step_back").click(function () {
        $("#li1Step").removeClass("active");
        $("#li2Step").addClass("active");
        $("#li3Step").removeClass("active");
    });
    

    $("#sec_step").click(function () {
        $("#li1Step").removeClass("active");
        $("#li2Step").removeClass("active");
        $("#li3Step").addClass("active");
    });

    $(".modal_open_status").click(function (event) {
        $("#OrderStatusId").val(($(this).attr('statusID')));
       $("#OrderIdJS").val(event.target.id);
    });



    $("#CustomerId").change(function () {
        if (this.value === '0') {

            $("#SelectInvoAdd").html("");
            $("#SelectShipAdd").html("");
            $(".collapseCustomer").show();
        } else {
            $(".collapseCustomer").hide();
            GetCustomerAddresses(this.value);
        }
    });

    $("#ship_invoice_chk").click(function () {

            copy_inv_to_ship();
    });

    var ob = $("#OrderDetailsForm_ManufacturerId");
    $.ajax({
        url: "/api/Manufacturers/",
        type: "GET",
        data: {},
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            $.each(response, function (i, item) {
                ob.prepend('<option value="' + item.id + '">' + item.name + '</option>');
            });
        }
    });

    $("#PostOrderDetails").click(function () {

        PostOrderDetails();
    });


});

function PostOrderDetails() {

    var i = $("#last_id_prod_details").val();

    i++;

    $('#prodDetailsTable tr:last').after('<tr><td><input class="form-control" type="text" id="OrderDetailsForm_ProductName" name="OrderDetails[' + i + '].ProductName" value=""></td><td><input class="form-control" type="text" id="OrderDetailsForm_ProductRef" name="OrderDetails[' + i + '].ProductRef" value=""></td><td><input class="form-control" type="text" id="OrderDetailsForm_Price" name="OrderDetails[' + i + '].Price" value=""></td><td><input class="form-control" type="text" id="OrderDetailsForm_PurchasePrice" name="OrderDetails[' + i + '].PurchasePrice" value=""></td><td><input class="form-control" type="number" id="OrderDetailsForm_Quantity" name="OrderDetails[' + i + '].Quantity" value=""><td><input class="form-control" type="checkbox" id="OrderDetailsForm_InStock" name="OrderDetails[' + i + '].InStock" value="true"></td></td>        <td><input class="form-control" type="datetime-local" id="OrderDetailsForm_ShipUndertakenDate" name="OrderDetails[' + i + '].ShipUndertakenDate" value=""></td><td> <input class="form-control" type="datetime-local" id="OrderDetailsForm_ShipExpectedDate" name="OrderDetails[' + i + '].ShipExpectedDate" value=""></td><td><select class="form-control" id="OrderDetailsForm_ManufacturerId' + i + '" name="OrderDetails[' + i + '].ManufacturerId"></select></td><td><input class="form-control" type="file" name="Image' + i + '"></td></tr>');


    var ob = $("#OrderDetailsForm_ManufacturerId" + i);
    $.ajax({
        url: "/api/Manufacturers/",
        type: "GET",
        data: {},
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            $.each(response, function (i, item) {
                ob.prepend('<option value="' + item.id + '">' + item.name + '</option>');
            });
        }
    });

    $("#last_id_prod_details").val(i);
}
            
            
            
function copy_inv_to_ship() {
    $("#ShipAddresses_FirstName").val($("#InvoiceAddresses_FirstName").val());
    $("#ShipAddresses_LastName").val($("#InvoiceAddresses_LastName").val());
    $("#ShipAddresses_Country").val($("#InvoiceAddresses_Country").val());
    $("#ShipAddresses_City").val($("#InvoiceAddresses_City").val());
    $("#ShipAddresses_Zipcode").val($("#InvoiceAddresses_Zipcode").val());
    $("#ShipAddresses_Address").val($("#InvoiceAddresses_Address").val());
    $("#ShipAddresses_Company").val($("#InvoiceAddresses_Company").val());
    $("#ShipAddresses_Phone").val($("#InvoiceAddresses_Phone").val());

}




function GetCustomerAddresses(id) {
    $.ajax({
        url: "/api/Customers/" + id,
        type: "GET",
        data: {  },
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {

            $invoSelect = "";
            $shipSelect = "";
            $.each(response.invoiceAddresses, function (i, item) {
                $invoSelect += '<option value="' + item.id + '">' + item.firstName + ' ' + item.lastName +' | ' + item.zipcode + ' | ' + item.city + ' | ' + item.address + ' | ' + item.company + ' | ' + item.phone + ' | ' + item.taxNumber + '</option>';
            });
            $.each(response.shipAddresses, function (i, item) {
                $shipSelect += '<option value="' + item.id + '">' + item.firstName + ' ' + item.lastName + ' | ' + item.zipcode + ' | ' + item.city + ' | ' + item.address + ' | ' + item.company + ' | ' + item.phone + ' </option>';
            });

            $invoHTML = '<label class="control-label" for= "invoiceAddressesId" > Számlázási Cím</label ><span class="requied"> *</span><select class="form-control "  id="InvoiceAddressId" name="InvoiceAddressId">' + $invoSelect+'</select>';
            $shipHTML = '<label class="control-label" for= "shipAddressesId" > Szállítási Cím</label ><span class="requied"> *</span><select class="form-control "  id="ShipAddressId" name="ShipAddressId" >' + $shipSelect + '</select>';
            $("#SelectInvoAdd").html($invoHTML);
            $("#SelectShipAdd").html($shipHTML);
            
        }
    });
}

function clear_input() {

}