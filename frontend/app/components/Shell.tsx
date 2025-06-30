// components/Shell.tsx
"use client";

import { ReactNode, useState } from "react";
import Sidebar from "./Sidebar";

export default function Shell({ children }: { children: ReactNode }) {
 // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [collapsed, setCollapsed] = useState(false);

  return (
    <div className="flex min-h-screen">
      <Sidebar/>

      <main
        className={`
          flex-1 p-6 transition-all duration-200
          ${collapsed ? "ml-16" : "ml-64"}
        `}
      >
        {children}
      </main>
    </div>
  );
}
