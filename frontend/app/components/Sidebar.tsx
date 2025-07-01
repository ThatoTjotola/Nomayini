// components/Sidebar.tsx
"use client";

import { useState } from "react";
import Link from "next/link";
import { usePathname } from "next/navigation";
import {Bars3Icon,HomeIcon,InformationCircleIcon,PencilSquareIcon,} from "@heroicons/react/24/outline";

const navItems = [
  { href: "/",      label: "Home",  icon: <HomeIcon className="h-6 w-6" /> },
  { href: "/about", label: "About me", icon: <InformationCircleIcon className="h-6 w-6" /> },
  { href: "/blog",  label: " My Blog",  icon: <PencilSquareIcon className="h-6 w-6" /> },
];

export default function Sidebar() {
  const [collapsed, setCollapsed] = useState(true);
  const path = usePathname();

  return (
    <aside
      className={`
        flex-shrink-0       
        transition-all duration-200
          bg-blue-50 dark:bg-gray-100 border-r dark:border-gray-700

        /* toggle width */
        ${collapsed ? "w-16" : "w-64"}
      `}
    >
      {/* header & toggle */}
      <div className="flex items-center justify-between p-4">
        {!collapsed && <span className="text-xl font-bold">Jimmys Portfolio</span>}
        <button
          onClick={() => setCollapsed((c) => !c)}
          className="p-1 rounded hover:bg-gray-100 dark:hover:bg-gray-900"
        >
          
          {collapsed ? <Bars3Icon className="h-6 w-6" /> : < Bars3Icon className="h-6 w-6" />}
        </button>
      </div>

      {/* nav */}
      <nav className="flex-1 overflow-auto">
        {navItems.map(({ href, label, icon }) => {
          const isActive = path === href;
          const base = "flex items-center px-4 py-2 mb-1 rounded-md transition-colors";
          const active = isActive
            ? "bg-gray-200 dark:bg-gray-700 font-semibold"
            : "hover:bg-gray-100 dark:hover:bg-gray-700";

          return (
            <Link key={href} href={href} className={`${base} ${active}`}>
              {icon}
              {!collapsed && <span className="ml-3">{label}</span>}
            </Link>
          );
        })}
      </nav>
    </aside>
  );
}
