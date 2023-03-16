var MISAapi = {
    "employee":{
        newEmployeeCode : "https://localhost:7116/api/v1/Employees/NewEmployeeCode",
        employeeApi: "https://localhost:7116/api/v1/Employees/",
        filter: "https://localhost:7116/api/v1/Employees/Filter?",
        deleteMultiple: "https://localhost:7116/api/v1/Employees/DeleteMultiple",
        export: "https://localhost:7116/api/v1/Employees/ExportExcel?"
    },
    "department":{
        departmentApi: "https://localhost:7116/api/v1/Departments/",
        filter: "https://localhost:7116/api/v1/Departments/Filter?pageSize=10&pageNumber=1"
    },
    "account":{
        base: "https://localhost:7116/api/v1/Accounts/",
        filter: "https://localhost:7116/api/v1/Accounts/Filter?",
    }
}
export default MISAapi;