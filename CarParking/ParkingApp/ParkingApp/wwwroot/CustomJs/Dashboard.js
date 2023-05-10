function Save() {
      $.ajax({
        type: "GET",
        url: '/Dashboard/BookParkingSpace',
        data: { zoneId: $("#ids").attr("zoneId"), spaceId: $("#ids").attr("spaceId"), bookingTime: $("#txt_book_time").val(), releaseTime: $("#txt_release_time").val() },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            var html = ""
           
        },
        error: function () {
            alert("Error while inserting data");
        }
    });
}
function getlist(object) {
    debugger;
    var basePath = 'https://localhost:44349/';
    var std = {};

    $.ajax({
        type: "GET",
        url: '/Dashboard/GetAllList',
        data: { id: $(object).val() }, 
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            var html = ""
            $.each(result, function (key, item) {
                html += '<tr zoneId=' + item.parking_Zone_Id + '  spaceId=' + item.id + '>';
                html += '<td>' + item.parking_Zone_Title + '</td>';

                html += '<td>' + item.parking_Space_Title + '</td>';
                if (item.is_Available == true) {
                    html += '<td><input type="button" class="btn btn-xs btn-success" value="Available"></td>';
                }
                else {
                    html += '<td><input type="button" class="btn btn-xs btn-danger" value="occupied"></td>';
                }
                if (item.is_Available == true) {
                    html += '<td><input type="button" class="btn btn-xs btn-primary" onclick="BookParking(this)" value="Click here to book"></td>';
                }

                html += '</tr>';

            });
            $('#space_table').DataTable().destroy();

            $('#space_table tbody').html(html);
            $('#space_table').DataTable({
                order: [[3, 'desc']],
            });
        },
        error: function () {
            alert("Error while inserting data");
        }
    });
} 
$(document).ready(function () {
    $("#txt_release_time").datetimepicker({
        format: 'DD-MM-YYYY HH:mm'
    });
    $("#txt_book_time").datetimepicker({
        format: 'DD-MM-YYYY HH:mm'
    });
});
function BookParking(object) {

   
    $("#txt_release_time").val('');
    $("#txt_book_time").val('');
    $("#lblZone").text($(object).closest('tr').find("td:eq(0)").text())
    $("#lblSpace").text($(object).closest('tr').find("td:eq(1)").text())
    $("#ids").attr("zoneId", $(object).closest('tr').attr("zoneId"))
    $("#ids").attr("spaceId", $(object).closest('tr').attr("spaceId"))
    $("#BookingModal").modal('show');
} 