import { useEffect, useState } from "react";
import MainLayout from "./layouts/MainLayout";
import TasksPage from "./pages/TasksPage";
import EmployeeStatusPage from "./pages/EmployeeStatusPage";
import LoginPage from "./pages/LoginPage";
import { getMe } from "./services/authService";
import HrSummaryPage from "./pages/HrSummaryPage";

interface User {
  id: number;
  displayName: string;
  role: "HR" | "Signer" | "Viewer";
  department?: string | null;
}

function App() {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  const loadUser = async () => {
    setLoading(true);
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        // No token: skip calling API (avoids 401 before login)
        setUser(null);
        return;
      }

      const me = await getMe();
      setUser(me);
    } catch {
      setUser(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadUser();
  }, []);

  if (loading) return <p>טוען…</p>;

  // 🔐 לא מחובר
  if (!user) {
    return <LoginPage onLoginSuccess={loadUser} />;
  }

  // 🧠 בחירת מסך לפי Role
  let content;

  if (user.role === "HR") {
    content = <HrSummaryPage />;
  } else if (user.role === "Signer") {
    content = <TasksPage department={user.department!} />;
  } else {
    content = <EmployeeStatusPage />;
  }

  return <MainLayout>{content}</MainLayout>;
}

export default App;
