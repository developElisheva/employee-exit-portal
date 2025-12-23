import { useState } from "react";
import { login } from "../services/authService";

interface Props {
  onLoginSuccess: () => void | Promise<void>;
}

export default function LoginPage({ onLoginSuccess }: Props) {
  const [tz, setTz] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const token = await login(tz, password);
      localStorage.setItem("token", token);
      await onLoginSuccess();
    } catch {
      setError("תעודת זהות או סיסמה שגויים");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-wrapper">
      <div className="login-card">
        <h2 className="form-title">כניסה למערכת</h2>

        <form onSubmit={handleSubmit} className="login-form">
          <div className="form-field">
            <label htmlFor="tz">תעודת זהות</label>
            <input
              id="tz"
              className="input"
              value={tz}
              onChange={(e) => setTz(e.target.value)}
              required
              aria-label="תעודת זהות"
            />
          </div>

          <div className="form-field">
            <label htmlFor="pw">סיסמה</label>
            <input
              id="pw"
              type="password"
              className="input"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              aria-label="סיסמה"
            />
          </div>

          {error && (
            <div className="error-text" role="alert">{error}</div>
          )}

          <button
            type="submit"
            disabled={loading}
            className="btn-primary"
          >
            {loading ? "מתחבר…" : "כניסה"}
          </button>
        </form>
      </div>
    </div>
  );
}
