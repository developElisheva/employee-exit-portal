import axios from "axios";

const httpClient = axios.create({
  baseURL: "https://localhost:7251/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default httpClient;
