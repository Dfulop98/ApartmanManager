import React from 'react'
import Link from 'next/link'
import styles from './Navbar.module.css'

import DappButton from './dappButton'
export default function Navbar() {

  return (
    <nav className={styles.navbar}>
      <div className={styles.home_container}>
        <Link href="/" className={styles.home}>Home</Link>
      </div>
      <div className={styles.gallery_container}>
        <Link href="/rooms" className={styles.gallery}>Gallery</Link>
      </div>
      <div className={styles.about_container}>
        <Link href="/about" className={styles.about}>About</Link>
      </div>
      <div className={styles.dapp_container}>
        <DappButton/>
      </div>
    </nav>
  )
}
