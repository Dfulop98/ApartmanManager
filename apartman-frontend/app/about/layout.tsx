import React from 'react'
import style from './style.module.css'
import type { Metadata } from 'next'

export const metadata: Metadata = {
    title: 'About',
    description: 'welcome to about page',
    }
export default function AboutLayout({
    children, 
  }: {
    children: React.ReactNode
  }) {
    return (
    <>
        <main className={style.main}>
            {children}
        </main>
    </>
  )
}
