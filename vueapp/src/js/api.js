var MISAapi = {
    "employee":{
        // newEmployeeCode : "https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode",
        // employeeApi: "https://cukcuk.manhnv.net/api/v1/Employees/",
        // filter: "https://cukcuk.manhnv.net/api/v1/Employees/filter?",
        newEmployeeCode : "https://localhost:7116/api/v1/Employees/NewEmployeeCode",
        employeeApi: "https://localhost:7116/api/v1/Employees/",
        filter: "https://localhost:7116/api/v1/Employees/Filter?",
        deleteMultiple: "https://localhost:7116/api/v1/Employees/DeleteMultiple",
        export: "https://localhost:7116/api/v1/Employees/ExportExcel"
    },
    "department":{
        // departmentApi: "https://cukcuk.manhnv.net/api/v1/Departments"
        departmentApi: "https://localhost:7116/api/v1/Departments/",
        filter: "https://localhost:7116/api/v1/Departments/Filter?pageSize=10&pageNumber=1"
    }
}
export default MISAapi;