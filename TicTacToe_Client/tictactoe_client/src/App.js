import {BrowserRouter, Routes, Route} from 'react-router-dom';

import MainPage from './components/pages/MainPage.js';
import TicTacToePage from './components/pages/TicTacToePage.js';
import MatchesPage from './components/pages/MatchesPage.js';
import HistoryPage from './components/pages/HistoryPage.js';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<MainPage />} />
        <Route path='matches' element={<MatchesPage />} />
        <Route path='tictactoe' element={<TicTacToePage />} />
        <Route path='history' element={<HistoryPage />} />
      </Routes>

    </BrowserRouter>
  );
}

export default App;


