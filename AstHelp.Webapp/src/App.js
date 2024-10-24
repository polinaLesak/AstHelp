import AppRouter from './app/AppRouter';
import ErrorToast from './shared/components/ErrorToast';
import Footer from './shared/components/Footer';
import LoadingOverlay from './shared/components/LoadingOverlay';
import Navbar from './shared/components/Navbar';

function App() {
  return (
    <div className="App">
      <LoadingOverlay />
      <ErrorToast />
      <Navbar />
      <AppRouter />
      <Footer />
    </div>
  );
}

export default App;
