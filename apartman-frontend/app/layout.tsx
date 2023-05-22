import './globals.css'
import {Inter} from 'next/font/google'
import type { Metadata } from 'next'

const inter = Inter({subsets:['latin']})
export const metadata: Metadata = {
  title: 'homepage',
  description: 'welcome to home page',
}
export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      {/*
        <head /> will contain the components returned by the nearest parent
        head.tsx. Find out more at https://beta.nextjs.org/docs/api-reference/file-conventions/head
      */}
      <head />
      <body className={inter.className}>
        <nav>
          <h1>MY NAVBAR</h1>
        </nav>
        {children}
      </body>
    </html>
  )
}
