"use client"; 

import Image from "next/image";
import { FormEvent, useState } from "react";

export default function Home() {
  const [prompt, setPrompt] = useState("");
  const [reply, setReply] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function askJimmy(e: FormEvent) {
    e.preventDefault();
    if (!prompt.trim()) return;

    setLoading(true);
    setError(null);
    setReply(null);

    try {
      const res = await fetch("https://ai.jimmytjotola.org/v1/completions", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          model: "gemma3n:e2b",
          prompt,
          max_tokens: 30,
        }),
      });
      if (!res.ok) throw new Error(`HTTP ${res.status}`);
      const data = await res.json();
      const text = data.choices?.[0]?.text?.trim() ?? "No reply";
      setReply(text);
   // eslint-disable-next-line @typescript-eslint/no-unused-vars, @typescript-eslint/no-explicit-any
    } catch (err: any) {
      setError(err.message || "Something went wrong");
    } finally {
      setLoading(false);
    }
  }

  return (
    <main className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center p-8 pb-20 gap-16 sm:p-20">
      {/* Row 2: form + reply */}
      <div className="row-start-2 flex flex-col gap-8 w-full max-w-md">
        <h1 className="text-2xl font-semibold tracking-tight mb-4">
          Welcome to my personal site. 
        </h1>
        <p className="text-sm/6 font-mono mb-6 text-center sm:text-left">
          Hi Xongotelo how are you !!! ,Ask my locally hosted chatbot about me or explore my site
        </p>

        <form onSubmit={askJimmy} className="space-y-4">
          <input
            value={prompt}
            onChange={(e) => setPrompt(e.target.value)}
            placeholder="Type your prompt here..."
            className="w-full px-4 py-2 text-sm text-gray-700 bg-white border rounded-md shadow-sm placeholder-gray-400
                       focus:border-blue-500 focus:ring-2 focus:ring-blue-500 focus:outline-none transition-colors"
          />
          <button
            type="submit"
            disabled={loading || !prompt.trim()}
            className="w-full flex items-center justify-center gap-2 py-2 bg-blue-600 text-white rounded-full
                       disabled:bg-red-600 hover:bg-red-700 transition-colors"
          >
            {loading ? (
              "Asking…"
            ) : (
              <>
                <Image
                  className="dark:invert"
                  src="/vercel.svg"
                  alt="✨"
                  width={20}
                  height={20}
                />
                Ask Jimmy
              </>
            )}
          </button>
        </form>

        {error && <p className="text-red-500 text-sm mt-2">Error: {error}</p>}

        {reply && (
          <div className="mt-6 p-4 bg-gray-100 dark:bg-gray-800 rounded-md">
            <h2 className="font-medium mb-2">Jimmy says:</h2>
            <p className="whitespace-pre-wrap">{reply}</p>
          </div>
        )}
      </div>

      {/* Row 3: footer */}
      <footer className="row-start-3 flex flex-wrap gap-6 justify-center">
        {/* your links… */}
      </footer>
    </main>
  );
}
