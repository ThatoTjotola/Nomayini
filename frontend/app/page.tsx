"use client" 
import Image from "next/image";
import { FormEvent, useState } from "react";

export default function Home() {
  const [prompt, setPrompt] = useState("")
  const [reply, setReply]       = useState<string | null>(null)
  const [loading, setLoading]   = useState(false)
  const [error, setError]       = useState<string | null>(null)

  async function askJimmy(e: FormEvent) {
    e.preventDefault()
    if (!prompt.trim()) return

    setLoading(true)
    setError(null)
    setReply(null)

    try {
      const res = await fetch(
        "https://ai.jimmytjotola.org/v1/completions",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            model:"gemma3n:e2b",
            prompt: prompt,
            max_tokens: 30
          }),
        }
      )

      if (!res.ok) {
        throw new Error(`HTTP ${res.status}`)
      }

      const data = await res.json()
      // for an OpenAI-style completions endpoint:
      // const text = data.choices?.[0]?.text?.trim()
      const text = data.choices?.[0]?.text?.trim() ?? "No reply"
      setReply(text)
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (err: any) {
      setError(err.message || "Something went wrong")
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
      <main className="flex flex-col gap-8 row-start-2 items-center sm:items-start w-full max-w-md">
        <h1 className="text-2xl font-semibold tracking-tight mb-4">
          Welcome to my personal site.
        </h1>
        <p className="list-inside list-decimal text-sm/6 text-center sm:text-left font-[family-name:var(--font-geist-mono)] mb-6">
          Ask my locally hosted chatbot about me or explore my site
        </p>

        <form onSubmit={askJimmy} className="w-full space-y-4">
          <input
            value={prompt}
            onChange={(e) => setPrompt(e.target.value)}
            placeholder="Type your prompt here..."
            className="block w-full px-4 py-2 text-sm text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm placeholder-gray-400
                       focus:border-blue-500 focus:ring-2 focus:ring-blue-500 focus:outline-none transition-colors"
          />

          <button
            type="submit"
            disabled={loading || !prompt.trim()}
            className="
              w-full flex items-center justify-center gap-2 py-2 bg-blue-600 text-white rounded-full
              disabled:bg-blue-300 hover:bg-blue-700 transition-colors
            "
          >
            {loading ? "Asking…" : (
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

        {error && (
          <p className="text-red-500 text-sm mt-2">Error: {error}</p>
        )}

        {reply && (
          <div className="mt-6 p-4 bg-gray-100 dark:bg-gray-800 rounded-md w-full">
            <h2 className="font-medium mb-2">Jimmy says:</h2>
            <p className="whitespace-pre-wrap">{reply}</p>
          </div>
        )}
      </main>
      <footer className="row-start-3 flex gap-[24px] flex-wrap items-center justify-center">
        <a
          className="flex items-center gap-2 hover:underline hover:underline-offset-4"
          href="https://nextjs.org/learn?utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            aria-hidden
            src="/file.svg"
            alt="File icon"
            width={16}
            height={16}
          />
          Learn
        </a>
        <a
          className="flex items-center gap-2 hover:underline hover:underline-offset-4"
          href="https://vercel.com/templates?framework=next.js&utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            aria-hidden
            src="/window.svg"
            alt="Window icon"
            width={16}
            height={16}
          />
          Examples
        </a>
        <a
          className="flex items-center gap-2 hover:underline hover:underline-offset-4"
          href="https://nextjs.org?utm_source=create-next-app&utm_medium=appdir-template-tw&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Image
            aria-hidden
            src="/globe.svg"
            alt="Globe icon"
            width={16}
            height={16}
          />
          Go to nextjs.org →
        </a>
      </footer>
    </div>
  );
}
