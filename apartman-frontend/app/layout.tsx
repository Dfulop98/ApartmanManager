import './globals.css'
import type { Metadata } from 'next'
import Navbar from '@/components/navbar/navbar'

import type { ReactNode, FC } from "react";

export const metadata: Metadata = {
  title: 'homepage',
  description: 'welcome to home page',

}

interface RootLayoutProps {
  children: ReactNode;
  confirmModal: ReactNode;
}

const RootLayout: FC<RootLayoutProps> = ({ children, confirmModal }) => {
  return (
    <html>
      <body>
        <Navbar />

        {children}
        {confirmModal}
      </body>
    </html>
  );
};

export default RootLayout;
