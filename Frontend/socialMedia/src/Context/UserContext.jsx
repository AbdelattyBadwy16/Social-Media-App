import React, { createContext, useState } from 'react'

export const User = createContext({});

export default function UserProvider({ children }) {

  const [user, setUser] = useState();
  let [status , setStatus] = useState(0);

  return (
    <User.Provider value={{user,setUser,status,setStatus}}>{children}</User.Provider>
  )
}
