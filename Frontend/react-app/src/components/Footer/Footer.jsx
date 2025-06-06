import './Footer.css';

const Footer = () => {
  const shouldShowFooter = process.env.REACT_APP_FOOTER === 'true';
  const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;

  if (!shouldShowFooter) {
    return null;
  }

  return (
    <footer className="footer">
      <div className="footer__content">
        <div className="footer__api-url">
          API Base URL: {apiBaseUrl}
        </div>
      </div>
    </footer>
  );
};

export default Footer;
