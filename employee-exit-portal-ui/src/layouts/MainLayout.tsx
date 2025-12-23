import type { ReactNode } from "react";
interface Props {
  children: ReactNode;
}

export default function MainLayout({ children }: Props) {
  return (
    <div>
      <header className="app-header">
        <div className="app-title">טופס טיולים</div>
      </header>

      <div className="app-root">
        <main className="app-content">
          {children}
        </main>
      </div>
    </div>
  );
}
