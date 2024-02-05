import React, { createContext, useState } from 'react'

export const postWindow = createContext(false);

export default function PostWindowProvider({ children }) {

  const [open, setOpen] = useState();

  return (
    <postWindow.Provider value={{open,setOpen}}>{children}</postWindow.Provider>
  )
}
