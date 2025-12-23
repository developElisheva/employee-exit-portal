import httpClient from "../api/httpClient";

interface LoginResponse {
  token: string;
}

export const login = async (tz: string, password: string): Promise<string> => {
  const res = await httpClient.post<LoginResponse>("/Auth/login", {
    tz,
    password,
  });

  return res.data.token;
};

export const getMe = async () => {
  const res = await httpClient.get("/Users/me");
  return res.data;
};
