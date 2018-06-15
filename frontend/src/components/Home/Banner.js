import React from 'react';

const style = { display: 'flex', justifyContent: 'center', alignItems: 'center', width: '..', height: '..'}

const Banner = ({ appName, token }) => {
  if (token) {
    return null;
  }

  return (
    <div className="banner">
      <div className="container">
        <div style={style}>
        <img src="https://preview.ibb.co/b30gKd/e_cicle_logo.png"
         alt="e_cicle_logo"  height="110" width="100" border="0"/>
         </div>
        </div>
        <div>
        <br/>
        <p>Sabe como reaproveitar o lixo eletrônico? Compartilhe!</p>
      </div>
    </div>
  );
};

export default Banner;
