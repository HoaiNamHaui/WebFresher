import MISAEnum from "./enum";
import MISAResource from "./resource";
const commonJS = {
  /**
   *
   * @param {*} errorCode
   */
  handleErrorCode(errorCode) {
    switch (errorCode) {
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
  // định dạng ngày
  formatDate(date) {
    try {
      if (date) {
        //Tạo đối tượng date
        date = new Date(date);
        //Khai báo biến year gán year hiện tại
        let year = date.getFullYear();
        //Khai báo biến month gán month vì month bắt đầu từ 0 nên ta + thêm 1
        let month = date.getMonth() + 1;
        //Dùng toán tử 3 ngôi để thêm 0 đằng trước ngày nhỏ hơn 10
        // month < 10 ? `0${month}` : `${month}`;
        if (month < 10) {
          month = "0" + month;
        } else {
          month = "" + month;
        }
        //Khai báo biến day và gán day bằng ngày hiện tại
        let day = date.getDate();
        //Dùng toán tử 3 ngôi để thêm 0 đằng trước ngày nhỏ hơn 10
        // day < 10 ? `0${day}` : `${day}`;
        if (day < 10) {
          day = "0" + day;
        } else {
          day = "" + day;
        }
        return `${day}/${month}/${year}`;
      } else {
        ("");
      }
    } catch (error) {
      console.log(error);
    }
  },
  // định dạng giới tính
  formatGender(gender) {
    if (gender == 0) {
      gender = "Nam";
    } else if (gender == 1) {
      gender = "Nữ";
    } else {
      gender = "Khác";
    }
  },
  // định dạng tiền
  formatMoney(money) {
    try {
      if (money) {
        money = new Intl.NumberFormat("vi-VI").format(money);
      } else {
        money = "0";
      }
      return money;
    } catch (error) {
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
