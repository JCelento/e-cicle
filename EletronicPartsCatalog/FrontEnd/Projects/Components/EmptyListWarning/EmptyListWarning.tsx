import * as React from 'react';

import './EmptyListWarning.scss';
import Button from '../../../Shared/Components/Button';

class EmptyListWarning extends React.Component<any, any>{
    
    private paths = {
    createProjectUrl: '/Projects/Add',
    createProjectLogo: ''
    };

    public render(){
    return(
        <section className='EmptyListWarning'>
        <img className='center-block' src='/images/List.png' />
        <section className='EmptyListWarning-textContainer'>
        <p className='text-center'>Ainda não temos nenhum projeto criado :(</p>
        <p className='text-center'>
     <Button link={true} href={this.paths.createProjectUrl} extraClassNames={['btn-success']}>
                            Criar um projeto
                        </Button>
                    </p>
                </section>
            </section>
        )
    }
}

export default EmptyListWarning;