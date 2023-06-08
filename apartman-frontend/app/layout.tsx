import './globals.css'
import type { Metadata } from 'next'
import Navbar from '@/components/navbar/navbar'

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
      <body >
        <Navbar/>
        {children}
      </body>
    </html>
  )
}
