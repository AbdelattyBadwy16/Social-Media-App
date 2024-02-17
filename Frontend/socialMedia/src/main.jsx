import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import UserProvider from './Context/UserContext.jsx'
import PostWindowProvider from './Context/PostWindow.jsx'
import UserPostProvider from './Context/UserPostContext.jsx'
import FavPostProvider from './Context/MyFavPost.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <PostWindowProvider>
      <UserProvider>
        <UserPostProvider>
          <FavPostProvider>
            <App />
          </FavPostProvider>
        </UserPostProvider>
      </UserProvider>
    </PostWindowProvider>
  </React.StrictMode>,
)
