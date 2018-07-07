import React from 'react';

const style = { display: 'flex', justifyContent: 'center', alignItems: 'center', width: '..', height: '..'}

const Description = ({ token }) => {
  if (token) {
    return null;
  }

  return (
      <div className="container">
        <div style={style}>
        <br/>
        <p>O e-cicle ajuda você a encontrar projetos com componentes eletrônicos reciclados
           e compartilhar seus projetos com o resto do mundo e amigos.</p>
         </div>
         </div>
  );
};

export default Description;