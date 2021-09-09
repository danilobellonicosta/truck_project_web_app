import http from "../http-common";

const getAll = () => {
  return http.get("api/truck");
};

const get = id => {
  return http.get(`api/truck/${id}`);
}; 

const create = data => {
  return http.post("api/truck", data);
};

const update = data => {
  return http.put("api/truck", data);
};

const remove = id => {
  return http.delete(`api/truck/${id}`);
};

export default {
  getAll,
  get,
  create,
  update,
  remove
};