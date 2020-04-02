import React, {Component} from 'react'
import {View, Panel, PanelHeader, FormLayout, Textarea, File, Checkbox, Button, Link, FormLayoutGroup} from '@vkontakte/vkui'
import Icon24Camera from '@vkontakte/icons/dist/24/camera';

class CreateView extends Component{

    state = {
        buttonEnabled:true
    }
    
    checkboxChanged(e){
        this.setState({buttonEnabled:!e.target.checked});
    }

    filesSelected(input){
        console.log(input)
        if(input.target.files.length > 5){
            alert("Нельзя приложить больше пяти файлов к посту.");
            input.target.value='';
            return;
        }
        const fileMaxSize = 7e+6; // 7 мегабайт в байтах
        for(var i=0;i<input.target.files.length;i++){
            if(input.target.files[i].size > fileMaxSize){
                alert(`Файл ${input.target.files[i].name} превышает максимально допустимый размер.`)
                input.target.value='';
                return;
            }
        }
    }

    handleSubmit(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        fetch('/posts', {
          method: 'POST',
          body: data,
        });
      }


    render(){
        const {id, fetchedUserId} = this.props;
        return (
            <View id={id} activePanel="create">
                <Panel id="create">
                    <PanelHeader>Создать объявление</PanelHeader>

                    <FormLayout id="myform" onSubmit={this.handleSubmit.bind(this)}>
                        <Textarea top="Текст сообщения" 
                                name="content"
                                placeholder="Введите текст"/>

                        <File name="attachments"
                            accept="image/png, image/jpeg"
                            multiple
                            top="Добавить фото"
                            before={<Icon24Camera/>} size="l"
                            onChange={this.filesSelected.bind(this)} >
                            Открыть галерею
                        </File>
                        <FormLayoutGroup>
                            <Checkbox name="isAnonymous">Опубликовать анонимно</Checkbox>
                            <Checkbox onChange={this.checkboxChanged.bind(this)}>Согласен со всем <Link>этим</Link></Checkbox>
                        </FormLayoutGroup>

                        <Button type="submit"
                                formEncType="multipart/form-data"
                                disabled={this.state.buttonEnabled} size="xl">
                            Опубликовать
                        </Button>

                        <input type="hidden" name="userId" value={fetchedUserId}/>
                    </FormLayout>

                </Panel>
            </View>
        )
    }
}

export default CreateView