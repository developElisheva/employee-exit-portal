import axios from "axios";

const httpClient = axios.create({
  baseURL: "https://localhost:7251/api",
  headers: {
    "Content-Type": "application/json",
  },
});

httpClient.interceptors.request.use(config => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default httpClient;
