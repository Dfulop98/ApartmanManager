'use client'
import Modal from '@/components/modal/modal'
import React from 'react'
import Styles from './style.module.css'
import { FormValues } from '@/types'
import Router from 'next/router'
export default function page() {
  
  
  const {
    query:data
  } = Router

  
  return (
    <Modal>
        <div className={Styles.container}>
            <p>
                successfully
            </p>
            {data.firstName}
        </div>
    </Modal>
  )
}
