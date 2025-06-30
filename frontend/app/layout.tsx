// app/layout.tsx
import "./globals.css";
import Sidebar from "./components/Sidebar";

export const metadata = {
  title: "Jimmy’s personal site",
  description: "Should help with job seeking",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className="antialiased">
        <div className="flex min-h-screen">
          {/* sidebar lives here once */}
          <Sidebar />

          {/* page.tsx just fills this */}
          <main className="flex-1 p-6">{children}</main>
        </div>
      </body>
    </html>
  );
}
