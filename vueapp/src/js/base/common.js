import MISAEnum from "./enum";
import MISAResource from "./resource";
const commonJS = {
  /**
   * 
   * @param {*} errorCode 
   */
  handleErrorCode(errorCode){
    switch(errorCode){
      case 404:
        console.log(MISAResource.vi.errorServerResponse[404]);
        break;
      case 400:
        console.log(MISAResource.vi.errorServerResponse[400]);
        break;
      case 500:
        console.log(MISAResource.vi.errorServerResponse[500]);
        break;
      default:
        console.log(MISAResource.vi.errorServerResponse.otherCode);
        break;
    }
  },

  /**
   * Định dạng ngày tháng : dd/mm/yyyy
   * CreatedBy: NHNam (28/12/2022)
   * @param {} date
   * @returns
   */
  formatDate(date) {
    try {
      if (date) {
        date = new Date(date);
        let newDate = date.getDate();
        let month = date.getMonth() + 1;
        let year = date.getFullYear();
        newDate = newDate < 10 ? `0${newDate}` : newDate;
        month = month < 10 ? `0${month}` : month;
        return `${newDate}/${month}/${year}`;
      } else return "";
    } catch (error) {
      console.log(error);
      return "";
    }
  },
  /**
   * chuyển từ enum ra text
   * @param {} gender 
   * @returns 
   */
  getTitleGender: (gender) => {
    var title = "Khác";
    switch (gender) {
      // Nam
      case MISAEnum.Gender.Male:
        title = "Nam";
        break;
      // Nữ
      case MISAEnum.Gender.Female:
        title = "Nữ";
        break;
      // Khác
      case MISAEnum.Gender.Other:
        title = "Khác";
        break;
      default:
        title = "";
        break;
    }
    return title;
  },
};
export default commonJS;
