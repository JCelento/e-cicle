import * as React from 'react';

import './EmptyListWarning.scss';
import Button from '../../../Shared/Components/Button';

class EmptyListWarning extends React.Component<any, any>{
    
    private paths = {
    createObjectUrl: '/Objects/Add',
    createObjectLogo: ''
    };

    public render(){
    return(
        <section className='EmptyListWarning'>
        <img className='center-block' src='/images/List.png' />
        <section className='EmptyListWarning-textContainer'>
        <p className='text-center'>Ainda não temos nenhum e-waste criado :(</p>
        <p className='text-center'>
     <Button link={true} href={this.paths.createObjectUrl} extraClassNames={['btn-success']}>
                            Cadastrar um e-waste
                        </Button>
                    </p>
                </section>
            </section>
        )
    }
}

export default EmptyListWarning;