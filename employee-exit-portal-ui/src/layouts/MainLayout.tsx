import type { ReactNode } from "react";
// styles removed

interface Props {
  sidebar: ReactNode;
  children: ReactNode;
}

export default function MainLayout({ sidebar, children }: Props) {
  return (
    <div>
      <header className="app-header">
        <div className="app-title">טופס טיולים</div>
      </header>

      <div className="app-root">
        <aside className="app-sidebar">{sidebar}</aside>
        <main className="app-content">{children}</main>
      </div>
    </div>
  );
}
