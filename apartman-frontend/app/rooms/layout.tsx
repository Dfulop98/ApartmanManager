import React from 'react'
import style from './style.module.css'
import type { Metadata } from 'next'

export const metadata: Metadata = {
    title: 'Room',
    description: 'welcome to room page',
    }
export default function RoomLayout({
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
