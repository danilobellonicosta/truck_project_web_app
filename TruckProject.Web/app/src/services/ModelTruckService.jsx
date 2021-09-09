import http from "../http-common";

const getAll = () => {
  return http.get("api/ModelTruck");
};

export default {
  getAll
};