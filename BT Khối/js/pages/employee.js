$(document).ready(function () {
  //load dữ liệu danh sách nhân viên
  loadData();
  //tạo sự kiện cho các thành phần trong page:
  initEvents();
});

/**
 * Load dữ liệu
 * CreatedBy: NHNAM (26/12/2022)
 */

function loadData() {
  try {
    $("#loading").show();
    // Gọi api thực hiện lấy dữ liệu:
    fetch("https://amis.manhnv.net/api/v1/Employees")
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        for (const Employee of data) {
          var EmployeeCode = Employee.EmployeeCode;
          var fullName = Employee.EmployeeName;
            var gender = Employee.GenderName || "";
            var dob = Employee.DateOfBirth || "";
            dob = formatDate(dob);
            var phone = Employee.PhoneNumber || "";
            var postition = Employee.EmployeePosition || "";
            var identityNumber = Employee.IdentityNumber || "";
            var departmentName = Employee.DepartmentName  || "";
            var bankAcc = Employee.BankAccountNumber || "";
            var bankName = Employee.BankName || "";
            var bankBranch = Employee.BankBranchName || "";
          let trHTML = `<tr>
                <td><input type="checkbox" name="" id=""></td>
                <td>${EmployeeCode}</td>
                <td>${fullName}</td>
                <td class="align-center">${dob}</td>
                <td>${gender}</td>
                <td>${phone}</td>
                <td>F${postition}</td>
                <td>${identityNumber}</td>
                <td>${departmentName}</td>
                <td>${bankAcc}</td>
                <td>${bankName}</td>
                <td>${bankBranch}</td>
                <td>Sửa
                    <div class="down-icon"></div>
                </td>
                 </tr>`;
            $("table#tblEmployee").append(trHTML); 
            $("#loading").hide();    
        }
      })
      .catch((error) => {
        console.log(error);
      });
    // Xử lí dữ liệu:
    // Định dạng dữ liệu ngày tháng
    // Định dạng tiền tệ
  } catch (error) {
    console.log(error);
  }
}
/**
 * Lập trình sự kiện cho các thành phần
 * CreatedBy: NHNAM (26/12/2022)
 */
function initEvents() {
  try {
    // Hiện Form chi tiết
    $("#addEmployee").click(function (e) {
      e.preventDefault();
      $("#overlay").show();
    });
    // Đóng form
    $("#btnClose").click(function (e) {
      e.preventDefault();
      $("#overlay").hide();
    });
    // Đóng form
    $("#btnCancel").click(function (e) {
      e.preventDefault();
      $("#overlay").hide();
    });
  } catch (error) {
    console.log(error);
  }
}
