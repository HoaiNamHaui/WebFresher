/**
 * Địnhndạng ngày tháng : dd/mm/yyyy
 * CreatedBy: NHNam (28/12/2022)
 * @param {} date 
 * @returns 
 */
function formatDate(date){
    try {
        if (date) {
          date = new Date(date);
          let newDate = date.getDate();
          let month = date.getMonth() + 1;
          let year = date.getFullYear();
          newDate = newDate < 10 ? `0${newDate}` : newDate;
          month = month < 10 ? `0${month}` : month;
          return `${newDate}/${month}/${year}`;
        }
        else
        return "";
      } catch (error) {
        console.log(error);
        return "";
      }
}

function formatMoney(){
    
}