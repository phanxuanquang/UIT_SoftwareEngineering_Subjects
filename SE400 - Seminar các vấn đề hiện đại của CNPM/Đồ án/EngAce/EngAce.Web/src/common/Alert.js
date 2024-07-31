import Swal from "sweetalert2";

const AlertCustom = ({ type, title }) => {
  if (type === "success") {
    return Swal.fire({
      width: "400",
      icon: "success",
      title: title,
      showConfirmButton: false,
      timer: 1000,
      timerProgressBar: true,
    });
  }

  if (type === "error") {
    Swal.fire({
      width: "400",
      icon: "error",
      title: title,
      showConfirmButton: false,
      timer: 2500,
      timerProgressBar: true,
    });
  }
};

export default AlertCustom;
