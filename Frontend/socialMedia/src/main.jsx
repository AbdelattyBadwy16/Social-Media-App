import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import UserProvider from './Context/UserContext.jsx'
import PostWindowProvider from './Context/PostWindow.jsx'
import UserPostProvider from './Context/UserPostContext.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <PostWindowProvider>
      <UserProvider>
        <UserPostProvider>
          <App />
        </UserPostProvider>
      </UserProvider>
    </PostWindowProvider>
  </React.StrictMode>,
)
